using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace senai.inlock.webApi.Controllers
{
    //Define que a rota de uma requisição será no seguinte formato
    //domino/api/Nomecontroller
    //ex: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Define que é  um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API sera formato JSON
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        { 
            _usuarioRepository = new UsuarioRepository();   
        }

        [HttpPost]
        public IActionResult Post(UsuarioDomain usuario)
        {
            try
            {
                //Cria um objeto que recebe os dados da requisicao
                UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioBuscado == null)
                {
                    return NotFound("Usuário ou Senha Inválidos");
                }

                //Caso encontre o usuário, prossegue para criação do token

                //1) - Defini as informações(claims) que serão fornecidas no token(PAYLOAD)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),


                    //Existe a possibilidade de criar uma claim personalizada
                    new Claim("Claim personalizada", "Valor da Claim personalizada")
                };

                //2) - Defini a chave de acesso ao Token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Inlock_games-chave-autenticacao-webapi-dev"));


                //3) - Defini as credenciais do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4) - Gerar o token 
                var token = new JwtSecurityToken
                (
                    //emissor do token
                    issuer: "senai.inlock.webApi",

                    //Destinatario de token
                    audience: "senai.inlock.webApi",

                    //dados definidos nas claims(Informações)
                    claims: claims,

                    //tempo de expiração de token
                    expires: DateTime.Now.AddMinutes(5),

                    //credencias do token
                    signingCredentials: creds
                );

                //5) - Retorna o token criado
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem erro
                return BadRequest(erro.Message);
            }
        }
    }
}

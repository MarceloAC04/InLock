using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Faz o login de acesso do usuario 
        /// </summary>
        /// <param name="email">Usuario cadastrado que sera logadp</param>
        UsuarioDomain Login(string email, string senha);
    }
}

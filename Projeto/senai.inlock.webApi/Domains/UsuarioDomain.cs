using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        public int IdTipoUsuario { get; set; }

        public string? Email { get; set; }

        [StringLength(20, MinimumLength = 4, ErrorMessage = "A senha dever ter de 4 a 20 caracteres")]
        public string? Senha { get; set; }
    }
}

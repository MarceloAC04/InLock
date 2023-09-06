using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    public class JogoDomain
    {
        public int IdJogo { get; set; }

        [Required(ErrorMessage = "A descrição do jogo é obrigatório")]
        public string? Descricao { get; set; }
        
        [Required(ErrorMessage = "O nome do jogo é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A data de lançamento do jogo é obrigatório")]
        public string? DataLancamento { get; set; }

        [Required(ErrorMessage = "O valor do jogo é obrigatório")]
        public int Valor { get; set; }

        public int IdEstudio { get; set; }

        //faz referencia a classe estudio
        public EstudioDomain Estudio { get; set; }
    }
}

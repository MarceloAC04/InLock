using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    public interface IJogoRepository
    {
        /// <summary>
        /// Lista todos os estudios cadastrados
        /// </summary>
        /// <returns>Lista com os objetos </returns>
        List<JogoDomain> ListarTodos();


        /// <summary>
        /// Cadastra um novo objeto jogo
        /// </summary>
        /// <param name="novoJogo">novo objeto jogo a ser cadastrado</param>
        void Cadastrar(JogoDomain novoJogo);

        /// <summary>
        /// Deleta um objeto jogo pelo seu id
        /// </summary>
        /// <param name="id">id do objeto a ser deletado</param>
        void Deletar(int id);
    }
}

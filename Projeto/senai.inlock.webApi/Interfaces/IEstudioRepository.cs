using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    public interface IEstudioRepository
    {

        /// <summary>
        /// Lista todos os estudios cadastrados
        /// </summary>
        /// <returns>Lista com os objetos </returns>
        List<EstudioDomain> ListarTodos();


        /// <summary>
        /// Cadastra um novo objeto estudio
        /// </summary>
        /// <param name="novoEstudio">novo objeto estudio a ser cadastrado</param>
        void Cadastrar(EstudioDomain novoEstudio);

        /// <summary>
        /// Deleta um objeto estudio pelo seu id
        /// </summary>
        /// <param name="id">id do objeto a ser deletado</param>
        void Deletar(int id);
    }
}

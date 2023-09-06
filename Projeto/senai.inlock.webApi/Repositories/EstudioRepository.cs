using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        /// <summary>
        /// String de conexao com o banco de dados que recebe os seguintes parametros
        /// Data Source : Nome do servidor
        /// Initial Catalog : Nome do Banco de dados
        /// Autenticacao:
        ///     -Windows : Intergrated Security = true
        ///     -SQLServer : User Id = sa Pwd = Senha
        /// </summary>
        private string StringConexao = "Data Source = NOTE23-S15; Initial Catalog = inlock_games_manha; User Id = sa; Pwd = Senai@134";
                                                                                            // Integrated Security = true";
        public void Cadastrar(EstudioDomain novoEstudio)
        {
            //Declara a conexao passando a StringConexao como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que sera executada
                string queryInsert = "INSERT INTO Estudio(Nome) VALUES (@Nome)";

                //Declara o SqlComand passando a query que sera executada e a conexao com o banco
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do parametro @Nome
                    cmd.Parameters.AddWithValue("@Nome", novoEstudio.Nome);

                    //Abre a conexao com o banco de dados
                    con.Open();

                    //Executa a query (queryINSERT)
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = $"DELETE FROM Estudio WHERE idEstudio = {id}";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EstudioDomain> ListarTodos()
        {
            //Cria uma lista de objetos tipo estudio
            List<EstudioDomain> listaEstudios = new List<EstudioDomain>();

            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                string querySelectAll = "SELECT IdEstudio, Nome FROM Estudio";

                //Abre a conexao com o banco de dados
                con.Open();

                ////Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                //Declara o SqlComand passando a query que sera executada e a conexao com o banco
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            //Atribui a propriedade IdEstudio o valor recebido no rdr
                            IdEstudio = Convert.ToInt32(rdr[0]),

                            //Atribui a propriedade Nome o valor recebido no rdr
                            Nome = rdr["Nome"].ToString()
                        };

                        //Adiciona cada objeto dentro da lista
                        listaEstudios.Add(estudio);
                    }
                }
            }
            //Retorna a lista de estudios
            return listaEstudios;
        }
    }
}

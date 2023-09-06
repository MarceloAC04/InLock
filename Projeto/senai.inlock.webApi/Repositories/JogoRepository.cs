using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repositories
{
    public class JogoRepository : IJogoRepository
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
        public void Cadastrar(JogoDomain novoJogo)
        {
            //Declara a conexao passando a StringConexao como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que sera executada
                string queryInsert = "INSERT INTO Jogo(Nome, Descricao, Valor, DataLancamento) VALUES (@Nome, @Descricao, @Valor, @DataLancamento)";

                //Declara o SqlComand passando a query que sera executada e a conexao com o banco
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do parametro @Nome
                    cmd.Parameters.AddWithValue("@Nome", novoJogo.Nome);

                    //Passa o valor do parametro @Descricao
                    cmd.Parameters.AddWithValue("@Descricao", novoJogo.Descricao);

                    //Passa o valor do parametro @Valor
                    cmd.Parameters.AddWithValue("@Valor", novoJogo.Valor);

                    //Passa o valor do parametro @DataLancamento
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);

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
                string queryDelete = $"DELETE FROM Jogo WHERE idJogo = {id}";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            //Cria uma lista de objetos tipo jogo
            List<JogoDomain> listaJogos = new List<JogoDomain>();

            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                string querySelectAll = "SELECT IdJogo , Jogo.Nome AS Jogo, Valor,  Descricao, Estudio.IdEstudio, DataLancamento , Valor , Estudio.Nome AS Estudio From Jogo INNER JOIN Estudio ON Jogo.IdEstudio = Estudio.IdEstudio;";

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
                        JogoDomain jogo = new JogoDomain()
                        {
                            //Atribui a propriedade IdJogo o valor recebido no rdr
                            IdJogo= Convert.ToInt32(rdr["Idjogo"]),

                            //Atribui a propriedade Nome o valor recebido no rdr
                            Nome = rdr["Jogo"].ToString(),

                            //Atribui a propriedade Descricao o valor recebido no rdr
                            Descricao = rdr["Descricao"].ToString(),

                            //Atribui a propriedade DataLancamento o valor recebido no rdr
                            DataLancamento = rdr["DataLancamento"].ToString(),

                            //Atribui a propriedade DataLancamento o valor recebido no rdr
                            Valor = Convert.ToInt32(rdr["Valor"]),

                            Estudio = new EstudioDomain()
                            {
                                IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                                Nome = rdr["Estudio"].ToString()
                            }


                        };

                        //Adiciona cada objeto dentro da lista
                        listaJogos.Add(jogo);
                    }
                }
            }
            //Retorna a lista de jogos
            return listaJogos;
        }
    }
}

using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private string StringConexao = "Data Source=LAB102501\\SALEEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=132;";

        public List<FuncionarioDomain> Listar()
        {

            List<FuncionarioDomain> exibir = new List<FuncionarioDomain>();


            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryexibir = "SELECT Funcionarios.IdFuncionarios, Funcionarios.Nome, Funcionarios.Sobrenome, DataNascimento FROM Funcionarios";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryexibir, con))
                {
                    con.Open();

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        FuncionarioDomain exi = new FuncionarioDomain
                        {
                            IdFuncionarios = Convert.ToInt32(rdr[0]),

                            Nome = rdr["Nome"].ToString(),

                            Sobrenome = rdr["Sobrenome"].ToString(),

                            DataNascimento = DateTime.Parse(rdr["DataNascimento"].ToString())
                        };

                        exibir.Add(exi);
                    }
                }
            }
            return exibir;
        }


        public FuncionarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryler = "SELECT IdFuncionarios, Nome, Sobrenome, DataNascimento FROM Funcionarios WHERE IdFuncionarios = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryler, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {

                        FuncionarioDomain cadas = new FuncionarioDomain
                        {

                            IdFuncionarios = Convert.ToInt32(rdr["IdFuncionarios"]),

                            Nome = rdr["Nome"].ToString(),

                            Sobrenome = rdr["Sobrenome"].ToString(),

                            DataNascimento = DateTime.Parse(rdr["DataNascimento"].ToString())
                           
                        };

                        return cadas;
                    }
                    return null;
                }
            }
        }

        public void Adicionar(FuncionarioDomain funcionarioRecebido)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = $"INSERT INTO Funcionarios (Nome, Sobrenome, DataNascimento) VALUES (@Nome, @Sobrenome, @DataNascimento)";
                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", funcionarioRecebido.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarioRecebido.Sobrenome);
                    cmd.Parameters.AddWithValue("@Datanascimento", funcionarioRecebido.DataNascimento);
                    rdr = cmd.ExecuteReader();
                }
            }
        }



        public void AtualizarIdUrl(int id, FuncionarioDomain cadastro)
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {

                string queryatt = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome  WHERE IdFuncionarios = @ID";


                using (SqlCommand cmd = new SqlCommand(queryatt, con))
                {

                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.Parameters.AddWithValue("@Nome", cadastro.Nome);

                    cmd.Parameters.AddWithValue("@Sobrenome", cadastro.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Funcionarios WHERE IdFuncionarios = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

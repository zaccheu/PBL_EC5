using System.Data.SqlClient;

namespace PBL_EC5.DAO
{
    // Colocar conexão com o banco de dados - adicionar connectionString e abrir a conexão
    public class ConexaoDB
    {
        public static SqlConnection GetConexao()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=PBL_EC5;user id=sa; password=123456";
            string connectionString = "Data Source=tmdc1dbdv01;Initial Catalog=teste_pbl;user id=sistemas; password=sistemas";
            SqlConnection conexao = new SqlConnection(connectionString);
            conexao.Open();
            return conexao;
        }
    }
}

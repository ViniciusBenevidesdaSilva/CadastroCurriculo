using System.Data.SqlClient;

namespace CadastroCurriculo.DAO
{
    public static class ConexaoDB
    {

        /// <summary>
        /// Método estático que retorna uma conexão aberta com o banco CurriculoDB
        /// </summary>
        /// <returns>Retorna uma SqlConnection aberta</returns>
        public static SqlConnection GetConnection()
        {
            //string strCon = "Data Source=LOCALHOST; Database=CurriculoDB; user id=sa; password=123456";
            string strCon = @"Data Source=DESKTOP-34G837R\SQLEXPRESS; Database=CurriculoDB; Integrated Security=True";
            
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();

            return conexao;
        }
    }
}

using CadastroCurriculo.Enums;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CadastroCurriculo.DAO
{
    public static class HelperDAO
    {
        /// <summary>
        /// Executa um comando sql (insert, update ou delete) em alguma tabela do banco CurriculoDB
        /// </summary>
        /// <param name="sql">Comando escrito em SQL</param>
        /// <param name="parametros">Vetor com os parametros do comando</param>
        public static void ExecutaSQL(string sql, SqlParameter[] parametros = null)
        {
            using(var conexao = ConexaoDB.GetConnection())
            {
                using(var comando = new SqlCommand(sql, conexao))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);

                    comando.ExecuteNonQuery();

                    conexao.Close();
                }
            }
        }


        /// <summary>
        /// Executa um comando SELECT sql em alguma tabela do banco CurriculoDB
        /// </summary>
        /// <param name="sql">Comando escrito em SQL</param>
        /// <param name="parametros">Vetor com os parametros do comando</param>
        /// <returns>Retorna um DataTable com os valores do Select</returns>
        public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros = null)
        {
            using (SqlConnection conexao = ConexaoDB.GetConnection())
            {
                using(SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);

                    DataTable retorno = new DataTable();
                    adapter.Fill(retorno);

                    conexao.Close();
                    return retorno;
                }
            }
        }

        /// <summary>
        /// Método usuado para retornar o último valor de Identity inserido em uma tabela
        /// Ou seja, após a inserção de um novo valor (Id = null), ele retorna qual valor de id foi atribuído àquele objeto
        /// </summary>
        /// <param name="tabela">Enum contendo o nome da tabela. No comando, ele atribuirá $'tb_{tabela}'</param>
        /// <returns>Retorna o último identity de uma tabela em específico</returns>
        public static int? RetornaUltimaIdentity(EnumTabelas tabela)
        {
            string sql =
                $"SELECT IDENT_CURRENT('tb_{tabela}') AS Id;";

            using (SqlConnection conexao = ConexaoDB.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    DataTable retorno = new DataTable();
                    adapter.Fill(retorno);

                    conexao.Close();

                    if (retorno.Rows.Count == 0)
                        return null;

                    var valor = retorno.Rows[0]["Id"];

                    if(valor == DBNull.Value)
                        return null;

                    return Convert.ToInt32(valor);
                }
            }
        }


        /// <summary>
        /// Realiza a conversão de um valor null (C#) para o DBNull.Value (SQL)
        /// </summary>
        /// <param name="value">Objeto que pode receber um valor null(C#)</param>
        /// <returns>Se o valor enviado for null, ele retorna DBNull, caso contrário retorna o próprio valor</returns>
        public static object NullAsDbNull(object value)
        {
            if (value == null)
                return DBNull.Value;

            return value;
        }

        /// <summary>
        /// Realiza a conversão do valor DBNull.Value (SQL) para uma string (null) (C#)
        /// </summary>
        /// <param name="value">Valor proveniente do SQL que pode ser DBNull.Value</param>
        /// <returns>Retorna uma string null caso o value seja DBNull.Value ou convertido para String</returns>
        public static string DbNullAsString(object value)
        {
            if (value == DBNull.Value)
                return null;

            return value.ToString();
        }

        /// <summary>
        /// Realiza a conversão do valor DBNull.Value (SQL) para o double? (null) (C#)
        /// </summary>
        /// <param name="value">Valor proveniente do SQL que pode ser DBNull.Value</param>
        /// <returns>Retorna um double? null caso o value seja DBNull.Value ou convertido para Double</returns>
        public static double? DbNullAsDouble(object value)
        {
            if (value == DBNull.Value)
                return null;

            return Convert.ToDouble(value);
        }
    }
}

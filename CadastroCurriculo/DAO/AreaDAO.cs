using CadastroCurriculo.AbstractClasses;
using CadastroCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CadastroCurriculo.DAO
{
    /// <summary>
    /// Area de atuação do usuário. Todo currículo deve possuir uma
    /// </summary>
    public class AreaDAO : AbstractDAO<AreaViewModel>
    {
        #region Metodos Privados

        /// <summary>
        /// Cria um vetor de SqlParameter a partir de um objeto AreaViewModel
        /// </summary>
        /// <param name="area">Objeto AreaViewModel que servirá de base para o retorno</param>
        /// <returns>Vetor de SqlParameter contendo os campos da area</returns>
        private SqlParameter[] CriaParametros(AreaViewModel area)
        {
            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter(nameof(area.Id_Area), area.Id_Area);
            p[1] = new SqlParameter(nameof(area.Desc_Area), area.Desc_Area);

            return p;
        }

        /// <summary>
        /// A partir do DataRow retornado do ExecutaSelect, gera um AreaViewModel
        /// </summary>
        /// <param name="registro">Registro de um DataTable retornado de um Select</param>
        /// <returns>Retorna um AreaViewModel com os dados do DataRow</returns>
        private AreaViewModel MontaModel(DataRow registro)
        {
            AreaViewModel retorno = new AreaViewModel();

            retorno.Id_Area = Convert.ToInt32(registro[nameof(retorno.Id_Area)]);
            retorno.Desc_Area = registro[nameof(retorno.Desc_Area)].ToString();

            return retorno;
        }


        #endregion Metodos Privados


        #region CRUD

        /// <summary>
        /// Insere um AreaViewModel na tabela dbo.tb_Area
        /// </summary>
        /// <param name="area">AreaViewModel que será Inserida</param>
        public override void Inserir(AreaViewModel area)
        {
            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Area (Desc_Area) " +
                "VALUES (@Desc_Area);";

            HelperDAO.ExecutaSQL(sql, CriaParametros(area));
        }

        /// <summary>
        /// Altera um AreaViewModel na tabela dbo.tb_Area
        /// </summary>
        /// <param name="area">AreaViewModel que será Alterada</param>
        public override void Alterar(AreaViewModel area)
        {
            string sql =
                "UPDATE tb_Area " +
                "SET Desc_Area = @Desc_Area " +
                "WHERE Id_Area = @Id_Area;";

            HelperDAO.ExecutaSQL(sql, CriaParametros(area));
        }

        /// <summary>
        /// Exclui um AreaViewModel na tabela dbo.tb_Area
        /// </summary>
        /// <param name="area">AreaViewModel que será excluída</param>
        public override void Excluir(AreaViewModel area)
        {
            string sql =
                $"DELETE FROM tb_Area WHERE Id_Area = {area.Id_Area};";

            HelperDAO.ExecutaSQL(sql);
        }

        /// <summary>
        /// Realiza a consulta de um elemento da tabela dbo.tb_Area a partir de seu Id
        /// </summary>
        /// <param name="Id">Id do elemento que se está pesquisando</param>
        /// <returns>Se encontrar o registro, retorna-o, caso contrário retorna null</returns>
        public override AreaViewModel Consulta(int Id)
        {
            string sql = 
                $"SELECT * FROM tb_Area WHERE Id_Area = {Id};";

            DataTable tabela = HelperDAO.ExecutaSelect(sql);

            if (tabela.Rows.Count == 0)
                return null;
            
            return MontaModel(tabela.Rows[0]);
        }

        /// <summary>
        /// Retorna uma lista com todos as Areas cadastradas na tabela dbo.tb_Area
        /// </summary>
        /// <returns>Lista com todas as Areas cadastradas ordenada pela descrição</returns>
        public override List<AreaViewModel> Listagem()
        {
            List<AreaViewModel> lista = new List<AreaViewModel>();

            string sql = $"SELECT * FROM tb_Area ORDER BY Desc_Area;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            
            return lista;
        }

        #endregion CRUD

    }
}

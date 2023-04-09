using CadastroCurriculo.AbstractClasses;
using CadastroCurriculo.Enums;
using CadastroCurriculo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CadastroCurriculo.DAO
{
    public class ExperienciaDAO : AbstractDAO<ExperienciaViewModel>
    {
        #region Metodos Privados

        /// <summary>
        /// Cria um vetor de SqlParameter a partir de um objeto ExperienciaViewModel
        /// </summary>
        /// <param name="experiencia">Objeto ExperienciaViewModel que servirá de base para o retorno</param>
        /// <returns>Vetor de SqlParameter contendo os campos da Experiencia</returns>
        private SqlParameter[] CriaParametros(ExperienciaViewModel experiencia)
        {
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter(nameof(experiencia.Id_Experiencia), experiencia.Id_Experiencia);
            p[1] = new SqlParameter("Id_Curriculo", experiencia.Curriculo.Id_Curriculo);
            p[2] = new SqlParameter(nameof(experiencia.Desc_Experiencia), experiencia.Desc_Experiencia);

            return p;
        }


        private SqlParameter[] CriaParametros(List<ExperienciaViewModel> experiencias)
        {
            // 3 Parâmetros para cada Experencia
            SqlParameter[] p = new SqlParameter[3 * experiencias.Count];

            int indiceAux = 0;

            for(int i = 0; i < experiencias.Count; i++)
            {
                p[indiceAux++] = new SqlParameter($"Id_Experiencia{i}", HelperDAO.NullAsDbNull(experiencias[i].Id_Experiencia));
                p[indiceAux++] = new SqlParameter($"Id_Curriculo{i}", experiencias[i].Curriculo.Id_Curriculo);
                p[indiceAux++] = new SqlParameter($"Desc_Experiencia{i}", experiencias[i].Desc_Experiencia);
            }

            return p;
        }

        /// <summary>
        /// A partir do DataRow retornado do ExecutaSelect, gera uma ExperienciaViewModel
        /// </summary>
        /// <param name="registro">Registro de um DataTable retornado de um Select</param>
        /// <param name="curriculo">Curriculo base que será inserido por referência no objeto ExperienciaViewModel</param>
        /// <returns>Retorna um ExperienciaViewModel com os dados do DataRow</returns>
        private ExperienciaViewModel MontaModel(DataRow registro, CurriculoViewModel curriculo = null)
        {
            ExperienciaViewModel retorno = new ExperienciaViewModel(curriculo);

            retorno.Id_Experiencia = Convert.ToInt32(registro[nameof(retorno.Id_Experiencia)]);
            retorno.Desc_Experiencia = registro[nameof(retorno.Desc_Experiencia)].ToString();

            // TODO
            // Validar se essa pesquisa não gera uma recursividade no código
            // Nesse momento de montar a model, a propriedade "Curriculo" dos objetos será atribuída na CurriculoDAO,
            // assim, eles todos apontarão para o mesmo objeto
            //retorno.Curriculo = (new CurriculoDAO()).Consulta(Convert.ToInt32(registro["Id_Curriculo"]));

            //retorno.Curriculo = curriculo; -> Já feito no construtor

            return retorno;
        }


        #endregion Metodos Privados


        #region CRUD

        /// <summary>
        /// Insere uma ExperienciaViewModel na tabela dbo.tb_Experiencia
        /// </summary>
        /// <param name="experiencia">ExperienciaViewModel que será Inserida</param>
        public override void Inserir(ExperienciaViewModel experiencia)
        {
            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Experiencia ([Id_Curriculo], [Desc_Experiencia]) " +
                "VALUES (@Id_Curriculo, @Desc_Experiencia);";

            HelperDAO.ExecutaSQL(sql, CriaParametros(experiencia));
            experiencia.Id_Experiencia = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Experiencia);
        }

        /// <summary>
        /// Insere uma lista de Experiencias na tabela dbo.tb_Experiencia
        /// </summary>
        /// <param name="experiencias">Lista de ExperienciaViewModel que serão inseridas</param>
        public void Inserir(List<ExperienciaViewModel> experiencias)
        {
            if (experiencias == null || experiencias.Count == 0)
                return;

            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Experiencia ([Id_Curriculo], [Desc_Experiencia]) " +
                "VALUES";

            for(int i = 0; i < experiencias.Count; i++)
            {
                sql += $" (@Id_Curriculo{i}, @Desc_Experiencia{i})";

                // Se for o último
                sql += (i == experiencias.Count - 1) ? ";" : ",";
            }

            HelperDAO.ExecutaSQL(sql, CriaParametros(experiencias));

            int ultimoId = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Experiencia).Value;

            for (int i = experiencias.Count - 1; i >= 0; i--)
                experiencias[i].Id_Experiencia = ultimoId--;
        }

        /// <summary>
        /// Altera um ExperienciaViewModel na tabela dbo.tb_Experiencia
        /// </summary>
        /// <param name="experiencia">ExperienciaViewModel que será Alterada</param>
        public override void Alterar(ExperienciaViewModel experiencia)
        {
            string sql =
                "UPDATE tb_Experiencia " +
                "SET Id_Curriculo = @Id_Curriculo, " +
                    "Desc_Experiencia = @Desc_Experiencia " +
                "WHERE Id_Experiencia = @Id_Experiencia;";

            HelperDAO.ExecutaSQL(sql, CriaParametros(experiencia));
        }

        /// <summary>
        /// Esse método é apenas usado internamente para montar a string de alteração de uma lista,
        /// Externamente, o usuário passa a lista inteira para validar quais são novos, foram excluídos ou alterados
        /// </summary>
        /// <param name="experiencias">Lista de Experiencias passada internamente</param>
        private void Alterar(List<ExperienciaViewModel> experiencias)
        {
            if (experiencias == null || experiencias.Count == 0)
                return;

            string sql = "";

            // Para cada registro, é necessário criar um update
            for (int i = 0; i < experiencias.Count; i++)
            {
                sql +=
                    "UPDATE tb_Experiencia " +
                    $"SET Id_Curriculo = @Id_Curriculo{i}, " +
                    $"Desc_Experiencia = @Desc_Experiencia{i} " +
                    $"WHERE Id_Experiencia = @Id_Experiencia{i}; ";
            }

            HelperDAO.ExecutaSQL(sql, CriaParametros(experiencias));
        }

        public void Alterar(List<ExperienciaViewModel> experiencias, ref CurriculoViewModel curriculo)
        {
            // Lista com todas presentes no banco daquele curriculo
            List<ExperienciaViewModel> experienciasAux = ExperienciasPorCurriculo(ref curriculo);


            // 1°, seleciono novas que não existiam no banco, e as salvo
            List<ExperienciaViewModel> novos = experiencias.Where(f => !experienciasAux.Select(x => x.Id_Experiencia).Contains(f.Id_Experiencia)).ToList();
            Inserir(novos);

            // 2°, seleciono as que existem e as altero
            List<ExperienciaViewModel> alterados = experiencias.Where(f => experienciasAux.Select(x => x.Id_Experiencia).Contains(f.Id_Experiencia)).ToList();
            Alterar(alterados);

            // 3°, seleciono as que foram excluídas e as removo do banco
            List<int?> excluidos = experienciasAux.Where(x => !experiencias.Select(f => f.Id_Experiencia).Contains(x.Id_Experiencia)).Select(x => x.Id_Experiencia).ToList();
            Excluir(excluidos);
        }

        /// <summary>
        /// Exclui uma ExperienciaViewModel na tabela dbo.tb_Experiencia
        /// </summary>
        /// <param name="experiencia">ExperienciaViewModel que será excluída</param>
        public override void Excluir(ExperienciaViewModel experiencia)
        {
            string sql =
                $"DELETE FROM tb_Experiencia WHERE Id_Experiencia = {experiencia.Id_Experiencia};";

            HelperDAO.ExecutaSQL(sql);
        }

        public void Excluir(List<int?> ids)
        {
            if (ids == null || ids.Count == 0)
                return;

            string sql =
                $"DELETE FROM tb_Experiencia WHERE Id_Experiencia IN (";

            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i] == null)
                {
                    if (i == ids.Count - 1)
                        sql += ");";

                    continue;
                }

                sql += $" {ids[i]}";

                // Se for o último
                sql += (i == ids.Count - 1) ? ");" : ",";
            }

            HelperDAO.ExecutaSQL(sql);
        }

        /// <summary>
        /// Realiza a consulta de um elemento da tabela dbo.tb_Experiencia a partir de seu Id
        /// </summary>
        /// <param name="Id">Id do elemento que se está pesquisando</param>
        /// <returns>Se encontrar o registro, retorna-o, caso contrário retorna null</returns>
        public override ExperienciaViewModel Consulta(int Id)
        {
            string sql =
                $"SELECT * FROM tb_Experiencia WHERE Id_Experiencia = {Id};";

            DataTable tabela = HelperDAO.ExecutaSelect(sql);

            if (tabela.Rows.Count == 0)
                return null;

            return MontaModel(tabela.Rows[0]);
        }

        /// <summary>
        /// Retorna uma lista com todos os Curriculos cadastradas na tabela dbo.tb_Curriculo
        /// </summary>
        /// <returns>Lista com todas os Curriculos cadastrados ordenada pelo Id</returns>
        public override List<ExperienciaViewModel> Listagem()
        {
            List<ExperienciaViewModel> lista = new List<ExperienciaViewModel>();

            string sql = $"SELECT * FROM tb_Experiencia ORDER BY Id_Experiencia;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }


        /// <summary>
        /// Retorna todas as Experiencias de um curriculo especifico 
        /// </summary>
        /// <param name="curriculo">Curriculo que se está pesquisando</param>
        /// <returns>Lista com as experiencias do curriculo</returns>
        public List<ExperienciaViewModel> ExperienciasPorCurriculo(ref CurriculoViewModel curriculo)
        {
            List<ExperienciaViewModel> lista = new List<ExperienciaViewModel>();

            string sql =
                $"SELECT * FROM tb_Experiencia " +
                $"WHERE Id_Curriculo = {curriculo.Id_Curriculo} " +
                $"ORDER BY Id_Experiencia;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro, curriculo));

            return lista;
        }
        #endregion CRUD
    }
}

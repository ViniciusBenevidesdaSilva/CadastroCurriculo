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
    public class IdiomaDAO : AbstractDAO<IdiomaViewModel>
    {
        #region Metodos Privados

        /// <summary>
        /// Cria um vetor de SqlParameter a partir de um objeto IdiomaViewModel
        /// </summary>
        /// <param name="idioma">Objeto IdiomaViewModel que servirá de base para o retorno</param>
        /// <returns>Vetor de SqlParameter contendo os campos do Idioma</returns>
        private SqlParameter[] CriaParametros(IdiomaViewModel idioma)
        {
            SqlParameter[] p = new SqlParameter[4];

            p[0] = new SqlParameter(nameof(idioma.Id_Idioma), idioma.Id_Idioma);
            p[1] = new SqlParameter("Id_Curriculo", idioma.Curriculo.Id_Curriculo);
            p[2] = new SqlParameter(nameof(idioma.Desc_Idioma), idioma.Desc_Idioma);
            p[3] = new SqlParameter("Grau_Proficiencia", (int)idioma.GrauProficiencia);

            return p;
        }

        private SqlParameter[] CriaParametros(List<IdiomaViewModel> idiomas)
        {
            // 4 Parâmetros para cada Idioma
            SqlParameter[] p = new SqlParameter[4 * idiomas.Count];

            int indiceAux = 0;

            for (int i = 0; i < idiomas.Count; i++)
            {
                p[indiceAux++] = new SqlParameter($"Id_Idioma{i}", HelperDAO.NullAsDbNull(idiomas[i].Id_Idioma));
                p[indiceAux++] = new SqlParameter($"Id_Curriculo{i}", idiomas[i].Curriculo.Id_Curriculo);
                p[indiceAux++] = new SqlParameter($"Desc_Idioma{i}", idiomas[i].Desc_Idioma);
                p[indiceAux++] = new SqlParameter($"Grau_Proficiencia{i}", (int)idiomas[i].GrauProficiencia);
            }

            return p;
        }

        /// <summary>
        /// A partir do DataRow retornado do ExecutaSelect, gera um IdiomaViewModel
        /// </summary>
        /// <param name="registro">Registro de um DataTable retornado de um Select</param>
        /// <param name="curriculo">Curriculo base que será inserido por referência no objeto IdiomaViewModel</param>
        /// <returns>Retorna um IdiomaViewModel com os dados do DataRow</returns>
        private IdiomaViewModel MontaModel(DataRow registro, CurriculoViewModel curriculo = null)
        {
            IdiomaViewModel retorno = new IdiomaViewModel(curriculo);

            retorno.Id_Idioma = Convert.ToInt32(registro[nameof(retorno.Id_Idioma)]);
            retorno.Desc_Idioma = registro[nameof(retorno.Desc_Idioma)].ToString();
            retorno.GrauProficiencia = (EnumProficienciaIdioma)Convert.ToInt32(registro["Grau_Proficiencia"]);

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
        /// Insere uma IdiomaViewModel na tabela dbo.tb_Idioma
        /// </summary>
        /// <param name="idioma">IdiomaViewModel que será Inserido</param>
        public override void Inserir(IdiomaViewModel idioma)
        {
            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Idioma ([Id_Curriculo], [Desc_Idioma], [Grau_Proficiencia]) " +
                "VALUES (@Id_Curriculo, @Desc_Idioma, @Grau_Proficiencia);";

            HelperDAO.ExecutaSQL(sql, CriaParametros(idioma));
            idioma.Id_Idioma = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Idioma);
        }

        /// <summary>
        /// Insere uma lista de Idiomas na tabela dbo.tb_Idioma
        /// </summary>
        /// <param name="idiomas">Lista de IdiomaViewModel que serão inseridos</param>
        public void Inserir(List<IdiomaViewModel> idiomas)
        {
            if (idiomas == null || idiomas.Count == 0)
                return;

            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Idioma ([Id_Curriculo], [Desc_Idioma], [Grau_Proficiencia]) " +
                "VALUES";

            for (int i = 0; i < idiomas.Count; i++)
            {
                sql += $" (@Id_Curriculo{i}, @Desc_Idioma{i}, @Grau_Proficiencia{i})";

                // Se for o último
                sql += (i == idiomas.Count - 1) ? ";" : ",";
            }

            HelperDAO.ExecutaSQL(sql, CriaParametros(idiomas));

            int ultimoId = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Idioma).Value;

            for (int i = idiomas.Count - 1; i >= 0; i--)
                idiomas[i].Id_Idioma = ultimoId--;
        }

        /// <summary>
        /// Altera um IdiomaViewModel na tabela dbo.tb_Idioma
        /// </summary>
        /// <param name="idioma">IdiomaViewModel que será Alterada</param>
        public override void Alterar(IdiomaViewModel idioma)
        {
            string sql =
                "UPDATE tb_Idioma " +
                "SET Id_Curriculo = @Id_Curriculo, " +
                "Desc_Idioma = @Desc_Idioma, " +
                "Grau_Proficiencia = @Grau_Proficiencia " +
                "WHERE Id_Idioma = @Id_Idioma;";

            HelperDAO.ExecutaSQL(sql, CriaParametros(idioma));
        }

        /// <summary>
        /// Esse método é apenas usado internamente para montar a string de alteração de uma lista,
        /// Externamente, o usuário passa a lista inteira para validar quais são novos, foram excluídos ou alterados
        /// </summary>
        /// <param name="idiomas">Lista de Formacoes passada internamente</param>
        private void Alterar(List<IdiomaViewModel> idiomas)
        {
            if (idiomas == null || idiomas.Count == 0)
                return;

            string sql = "";

            // Para cada registro, é necessário criar um update
            for (int i = 0; i < idiomas.Count; i++)
            {
                sql +=
                    "UPDATE tb_Idioma " +
                    $"SET Id_Curriculo = @Id_Curriculo{i}, " +
                    $"Desc_Idioma = @Desc_Idioma{i}, " +
                    $"Grau_Proficiencia = @Grau_Proficiencia{i} " +
                    $"WHERE Id_Idioma = @Id_Idioma{i}; ";
            }

            HelperDAO.ExecutaSQL(sql, CriaParametros(idiomas));
        }

        public void Alterar(List<IdiomaViewModel> idiomas, ref CurriculoViewModel curriculo)
        {
            // Lista com todas as formações presentes no banco daquele curriculo
            List<IdiomaViewModel> idiomasAux = IdiomasPorCurriculo(ref curriculo);


            // 1°, seleciono as novas que não existiam no banco, e as salvo
            List<IdiomaViewModel> novos = idiomas.Where(f => !idiomasAux.Select(x => x.Id_Idioma).Contains(f.Id_Idioma)).ToList();
            Inserir(novos);

            // 2°, seleciono as que existem e as altero
            List<IdiomaViewModel> alterados = idiomas.Where(f => idiomasAux.Select(x => x.Id_Idioma).Contains(f.Id_Idioma)).ToList();
            Alterar(alterados);

            // 3°, seleciono as que foram excluídas e as removo do banco
            List<int?> excluidos = idiomasAux.Where(x => !idiomas.Select(f => f.Id_Idioma).Contains(x.Id_Idioma)).Select(x => x.Id_Idioma).ToList();
            Excluir(excluidos);
        }


        /// <summary>
        /// Exclui uma IdiomaViewModel na tabela dbo.tb_Idioma
        /// </summary>
        /// <param name="idioma">IdiomaViewModel que será excluído</param>
        public override void Excluir(IdiomaViewModel idioma)
        {
            string sql =
                $"DELETE FROM tb_Idioma WHERE Id_Idioma = {idioma.Id_Idioma};";

            HelperDAO.ExecutaSQL(sql);
        }

        public void Excluir(List<int?> ids)
        {
            if (ids == null || ids.Count == 0)
                return;

            string sql =
                $"DELETE FROM tb_Idioma WHERE Id_Idioma IN (";

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
        /// Realiza a consulta de um elemento da tabela dbo.tb_Idioma a partir de seu Id
        /// </summary>
        /// <param name="Id">Id do elemento que se está pesquisando</param>
        /// <returns>Se encontrar o registro, retorna-o, caso contrário retorna null</returns>
        public override IdiomaViewModel Consulta(int Id)
        {
            string sql =
                $"SELECT * FROM tb_Idioma WHERE Id_Idioma = {Id};";

            DataTable tabela = HelperDAO.ExecutaSelect(sql);

            if (tabela.Rows.Count == 0)
                return null;

            return MontaModel(tabela.Rows[0]);
        }

        /// <summary>
        /// Retorna uma lista com todos os Idiomas cadastradas na tabela dbo.tb_Idioma
        /// </summary>
        /// <returns>Lista com todas os Idiomas cadastrados ordenada pelo Id</returns>
        public override List<IdiomaViewModel> Listagem()
        {
            List<IdiomaViewModel> lista = new List<IdiomaViewModel>();

            string sql = $"SELECT * FROM tb_Idioma ORDER BY Id_Idioma;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

        /// <summary>
        /// Retorna todas os Idiomas de um curriculo especifico 
        /// </summary>
        /// <param name="curriculo">Curriculo que se está pesquisando</param>
        /// <returns>Lista com os Idiomas do curriculo</returns>
        public List<IdiomaViewModel> IdiomasPorCurriculo(ref CurriculoViewModel curriculo)
        {
            List<IdiomaViewModel> lista = new List<IdiomaViewModel>();

            string sql =
                $"SELECT * FROM tb_Idioma " +
                $"WHERE Id_Curriculo = {curriculo.Id_Curriculo} " +
                $"ORDER BY Id_Idioma;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro, curriculo));

            return lista;
        }

        #endregion CRUD
    }
}

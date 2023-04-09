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
    public class FormacaoDAO : AbstractDAO<FormacaoViewModel>
    {
        #region Metodos Privados

        /// <summary>
        /// Cria um vetor de SqlParameter a partir de um objeto FormacaoViewModel
        /// </summary>
        /// <param name="formacao">Objeto FormacaoViewModel que servirá de base para o retorno</param>
        /// <returns>Vetor de SqlParameter contendo os campos da formacao</returns>
        private SqlParameter[] CriaParametros(FormacaoViewModel formacao)
        {
            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter(nameof(formacao.Id_Formacao), formacao.Id_Formacao);
            p[1] = new SqlParameter("Id_Curriculo", formacao.Curriculo.Id_Curriculo);
            p[2] = new SqlParameter(nameof(formacao.Desc_Formacao), formacao.Desc_Formacao);
         
            return p;
        }

        private SqlParameter[] CriaParametros(List<FormacaoViewModel> formacoes)
        {
            // 3 Parâmetros para cada Formacao
            SqlParameter[] p = new SqlParameter[3 * formacoes.Count];

            int indiceAux = 0;

            for (int i = 0; i < formacoes.Count; i++)
            {
                p[indiceAux++] = new SqlParameter($"Id_Formacao{i}", HelperDAO.NullAsDbNull(formacoes[i].Id_Formacao));
                p[indiceAux++] = new SqlParameter($"Id_Curriculo{i}", formacoes[i].Curriculo.Id_Curriculo);
                p[indiceAux++] = new SqlParameter($"Desc_Formacao{i}", formacoes[i].Desc_Formacao);
            }

            return p;
        }

        /// <summary>
        /// A partir do DataRow retornado do ExecutaSelect, gera um FormacaoViewModel
        /// </summary>
        /// <param name="registro">Registro de um DataTable retornado de um Select</param>
        /// <param name="curriculo">Curriculo base que será inserido por referência no objeto FormacaoViewModel</param>
        /// <returns>Retorna um FormacaoViewModel com os dados do DataRow</returns>
        private FormacaoViewModel MontaModel(DataRow registro, CurriculoViewModel curriculo = null)
        {
            FormacaoViewModel retorno = new FormacaoViewModel(curriculo);

            retorno.Id_Formacao = Convert.ToInt32(registro[nameof(retorno.Id_Formacao)]);
            retorno.Desc_Formacao = registro[nameof(retorno.Desc_Formacao)].ToString();

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
        /// Insere uma FormacaoViewModel na tabela dbo.tb_Formacao
        /// </summary>
        /// <param name="formacao">FormacaoViewModel que será Inserida</param>
        public override void Inserir(FormacaoViewModel formacao)
        {
            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Formacao ([Id_Curriculo], [Desc_Formacao]) " +
                "VALUES (@Id_Curriculo, @Desc_Formacao);";

            HelperDAO.ExecutaSQL(sql, CriaParametros(formacao));
            formacao.Id_Formacao = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Formacao);
        }

        /// <summary>
        /// Insere uma lista de Formacoes na tabela dbo.tb_Formacao
        /// </summary>
        /// <param name="formacoes">Lista de FormacaoViewModel que serão inseridas</param>
        public void Inserir(List<FormacaoViewModel> formacoes)
        {
            if (formacoes == null || formacoes.Count == 0)
                return;


            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Formacao ([Id_Curriculo], [Desc_Formacao]) " +
                "VALUES";

            for (int i = 0; i < formacoes.Count; i++)
            {
                sql += $" (@Id_Curriculo{i}, @Desc_Formacao{i})";

                // Se for o último
                sql += (i == formacoes.Count - 1) ? ";" : ",";
            }

            HelperDAO.ExecutaSQL(sql, CriaParametros(formacoes));
            int ultimoId = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Formacao).Value;

            for(int i = formacoes.Count - 1; i >= 0; i--)
                formacoes[i].Id_Formacao = ultimoId--;
        }

        /// <summary>
        /// Altera um FormacaoViewModel na tabela dbo.tb_Formacao
        /// </summary>
        /// <param name="formacao">FormacaoViewModel que será Alterada</param>
        public override void Alterar(FormacaoViewModel formacao)
        {
            string sql =
                "UPDATE tb_Formacao " +
                "SET Id_Curriculo = @Id_Curriculo, " +
                    "Desc_Formacao = @Desc_Formacao " +
                "WHERE Id_Formacao = @Id_Formacao;";

            HelperDAO.ExecutaSQL(sql, CriaParametros(formacao));
        }

        /// <summary>
        /// Esse método é apenas usado internamente para montar a string de alteração de uma lista,
        /// Externamente, o usuário passa a lista inteira para validar quais são novos, foram excluídos ou alterados
        /// </summary>
        /// <param name="formacoes">Lista de Formacoes passada internamente</param>
        private void Alterar(List<FormacaoViewModel> formacoes)
        {
            if (formacoes == null || formacoes.Count == 0)
                return;

            string sql = "";

            // Para cada registro, é necessário criar um update
            for (int i = 0; i < formacoes.Count; i++)
            {
                sql += 
                    "UPDATE tb_Formacao " +
                    $"SET Id_Curriculo = @Id_Curriculo{i}, " +
                    $"Desc_Formacao = @Desc_Formacao{i} " +
                    $"WHERE Id_Formacao = @Id_Formacao{i}; ";
            }

            HelperDAO.ExecutaSQL(sql, CriaParametros(formacoes));
        }

        public void Alterar(List<FormacaoViewModel> formacoes, ref CurriculoViewModel curriculo)
        {
            // Lista com todas os presentes no banco daquele curriculo
            List<FormacaoViewModel> formacoesAux = FormacoesPorCurriculo(ref curriculo);


            // 1°, seleciono as formações novas que não existiam no banco, e as salvo
            List<FormacaoViewModel> novos = formacoes.Where(f => !formacoesAux.Select(x => x.Id_Formacao).Contains(f.Id_Formacao)).ToList();
            Inserir(novos);

            // 2°, seleciono as que existem e as altero
            List<FormacaoViewModel> alterados = formacoes.Where(f => formacoesAux.Select(x => x.Id_Formacao).Contains(f.Id_Formacao)).ToList();
            Alterar(alterados);

            // 3°, seleciono as que foram excluídas e as removo do banco
            List<int?> excluidos = formacoesAux.Where(x => !formacoes.Select(f => f.Id_Formacao).Contains(x.Id_Formacao)).Select(x => x.Id_Formacao).ToList();
            Excluir(excluidos);
        }

        /// <summary>
        /// Exclui uma FormacaoViewModel na tabela dbo.tb_Formacao
        /// </summary>
        /// <param name="formacao">FormacaoViewModel que será excluída</param>
        public override void Excluir(FormacaoViewModel formacao)
        {
            string sql =
                $"DELETE FROM tb_Formacao WHERE Id_Formacao = {formacao.Id_Formacao};";

            HelperDAO.ExecutaSQL(sql);
        }

        public void Excluir(List<int?> ids)
        {
            if (ids == null || ids.Count == 0)
                return;

            string sql =
                $"DELETE FROM tb_Formacao WHERE Id_Formacao IN (";

            for(int i = 0; i < ids.Count; i++)
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
        /// Realiza a consulta de um elemento da tabela dbo.tb_Formacao a partir de seu Id
        /// </summary>
        /// <param name="Id">Id do elemento que se está pesquisando</param>
        /// <returns>Se encontrar o registro, retorna-o, caso contrário retorna null</returns>
        public override FormacaoViewModel Consulta(int Id)
        {
            string sql = 
                $"SELECT * FROM tb_Formacao WHERE Id_Formacao = {Id};";

            DataTable tabela = HelperDAO.ExecutaSelect(sql);

            if (tabela.Rows.Count == 0)
                return null;

            return MontaModel(tabela.Rows[0]);
        }

        /// <summary>
        /// Retorna uma lista com todos os Curriculos cadastradas na tabela dbo.tb_Curriculo
        /// </summary>
        /// <returns>Lista com todas os Curriculos cadastrados ordenada pelo Id</returns>
        public override List<FormacaoViewModel> Listagem()
        {
            List<FormacaoViewModel> lista = new List<FormacaoViewModel>();

            string sql = $"SELECT * FROM tb_Formacao ORDER BY Id_Formacao;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

        /// <summary>
        /// Retorna todas as Formacaos de um curriculo especifico 
        /// </summary>
        /// <param name="curriculo">Curriculo que se está pesquisando</param>
        /// <returns>Lista com as formações do curriculo</returns>
        public List<FormacaoViewModel> FormacoesPorCurriculo(ref CurriculoViewModel curriculo)
        {
            List<FormacaoViewModel> lista = new List<FormacaoViewModel>();

            string sql = 
                $"SELECT * FROM tb_Formacao " +
                $"WHERE Id_Curriculo = {curriculo.Id_Curriculo} " +
                $"ORDER BY Id_Formacao;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro, curriculo));

            return lista;
        }

        #endregion CRUD
    }
}

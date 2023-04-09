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
    public class CurriculoDAO : AbstractDAO<CurriculoViewModel>
    {
        #region DAOs

        AreaDAO areaDAO = new AreaDAO();

        FormacaoDAO formacaoDAO = new FormacaoDAO();
        ExperienciaDAO experienciaDAO = new ExperienciaDAO();
        IdiomaDAO idiomaDAO = new IdiomaDAO();

        #endregion DAOs



        #region Metodos Privados

        /// <summary>
        /// Cria um vetor de SqlParameter a partir de um objeto CurriculoViewModel
        /// </summary>
        /// <param name="curriculo">Objeto CurriculoViewModel que servirá de base para o retorno</param>
        /// <returns>Vetor de SqlParameter contendo os campos do curriculo</returns>
        private SqlParameter[] CriaParametros(CurriculoViewModel curriculo)
        {
            SqlParameter[] p = new SqlParameter[10];

            p[0] = new SqlParameter(nameof(curriculo.Id_Curriculo), HelperDAO.NullAsDbNull(curriculo.Id_Curriculo));
            p[1] = new SqlParameter("Id_Area", curriculo.Area.Id_Area);

            p[2] = new SqlParameter(nameof(curriculo.Nome), curriculo.Nome);
            p[3] = new SqlParameter(nameof(curriculo.CPF), curriculo.CPF);
            p[4] = new SqlParameter(nameof(curriculo.Endereco), HelperDAO.NullAsDbNull(curriculo.Endereco));
            p[5] = new SqlParameter(nameof(curriculo.Telefone), HelperDAO.NullAsDbNull(curriculo.Telefone));
            p[6] = new SqlParameter(nameof(curriculo.Email), HelperDAO.NullAsDbNull(curriculo.Email));
            p[7] = new SqlParameter(nameof(curriculo.Cargo_Pretendido), curriculo.Cargo_Pretendido);
            p[8] = new SqlParameter(nameof(curriculo.Id_Icone), curriculo.Id_Icone);
            p[9] = new SqlParameter(nameof(curriculo.Pretensao_Salarial), HelperDAO.NullAsDbNull(curriculo.Pretensao_Salarial));

            return p;
        }

        /// <summary>
        /// Retorna o vetor de SqlParameter exclusivo para ser usado na Pesquisa por Nome ou Area
        /// </summary>
        /// <param name="Nome">Nome pesquisado</param>
        /// <param name="Id_Area">Id da área pesquisada</param>
        /// <returns>Vetor de SqlParameter contendo o Nome e Id da àrea</returns>
        private SqlParameter[] CriaParametros(string Nome, int? Id_Area)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("Nome", HelperDAO.NullAsDbNull(Nome));
            p[1] = new SqlParameter("Id_Area", HelperDAO.NullAsDbNull(Id_Area));

            return p;
        }

        /// <summary>
        /// A partir do DataRow retornado do ExecutaSelect, gera um CurriculoViewModel
        /// </summary>
        /// <param name="registro">Registro de um DataTable retornado de um Select</param>
        /// <returns>Retorna um CurriculoViewModel com os dados do DataRow</returns>
        private CurriculoViewModel MontaModel(DataRow registro)
        {
            CurriculoViewModel retorno = new CurriculoViewModel();

            retorno.Id_Curriculo = Convert.ToInt32(registro[nameof(retorno.Id_Curriculo)]);

            // Nesse caso, ele consultará qual a área pelo Id registrado
            // Mesmo instanciando um novo objeto, quando for salvar irá apenas registrar
            // esse Id na tabela tb_Curriculo, não alterando a tabela tb_Area
            retorno.Area = areaDAO.Consulta(Convert.ToInt32(registro["Id_Area"]));

            retorno.Nome = registro[nameof(retorno.Nome)].ToString();
            retorno.CPF = registro[nameof(retorno.CPF)].ToString();
            retorno.Endereco = HelperDAO.DbNullAsString(registro[nameof(retorno.Endereco)]);
            retorno.Telefone = HelperDAO.DbNullAsString(registro[nameof(retorno.Telefone)]);
            retorno.Email = HelperDAO.DbNullAsString(registro[nameof(retorno.Email)]);
            retorno.Cargo_Pretendido = registro[nameof(retorno.Cargo_Pretendido)].ToString();

            retorno.Id_Icone = Convert.ToInt32(registro[nameof(retorno.Id_Icone)]);
            retorno.Pretensao_Salarial = HelperDAO.DbNullAsDouble(registro[nameof(retorno.Pretensao_Salarial)]);

            // TODO
            // Necessário pesquisar nas outras classes DAO pelos elementos associados à esse curriculo
            // Nesse momento de montar a model, a propriedade "Curriculo" dos objetos será atribuída aqui,
            // assim, eles todos apontarão para o mesmo objeto

            retorno.Formacoes = formacaoDAO.FormacoesPorCurriculo(ref retorno);
            retorno.Experiencias = experienciaDAO.ExperienciasPorCurriculo(ref retorno);
            retorno.Idiomas = idiomaDAO.IdiomasPorCurriculo(ref retorno);

            return retorno;
        }


        #endregion Metodos Privados


        #region CRUD

        /// <summary>
        /// Insere um CurriculoViewModel na tabela dbo.tb_Curriculo
        /// </summary>
        /// <param name="curriculo">CurriculoViewModel que será Inserido</param>
        public override void Inserir(CurriculoViewModel curriculo)
        {
            // Id_Area é gerado por uma Identity(1,1)
            string sql =
                "INSERT INTO tb_Curriculo ([Id_Area], [Nome], [CPF], [Endereco], [Telefone], [Email], [Cargo_Pretendido], [Id_Icone], [Pretensao_Salarial]) " +
                "VALUES (@Id_Area, @Nome, @CPF, @Endereco, @Telefone, @Email, @Cargo_Pretendido, @Id_Icone, @Pretensao_Salarial);";

            HelperDAO.ExecutaSQL(sql, CriaParametros(curriculo));
            curriculo.Id_Curriculo = HelperDAO.RetornaUltimaIdentity(EnumTabelas.Curriculo);

            // TODO
            // Após inserir o currículo, é necessário iterar pelos elementos das listas e chamar suas respectivas classesDao para salvá-los
            formacaoDAO.Inserir(curriculo.Formacoes);
            experienciaDAO.Inserir(curriculo.Experiencias);
            idiomaDAO.Inserir(curriculo.Idiomas);

        }

        /// <summary>
        /// Altera um CurriculoViewModel na tabela dbo.tb_Curriculo
        /// </summary>
        /// <param name="curriculo">CurriculoViewModel que será Alterado</param>
        public override void Alterar(CurriculoViewModel curriculo)
        {
            string sql =
                "UPDATE tb_Curriculo " +
                "SET Id_Area = @Id_Area, " +
                    "Nome = @Nome, " +
                    "CPF = @CPF, " +
                    "Endereco = @Endereco, " +
                    "Telefone = @Telefone, " +
                    "Email = @Email, " +
                    "Cargo_Pretendido = @Cargo_Pretendido, " +
                    "Id_Icone = @Id_Icone, " +
                    "Pretensao_Salarial = @Pretensao_Salarial " +

                "WHERE Id_Curriculo = @Id_Curriculo;";

            HelperDAO.ExecutaSQL(sql, CriaParametros(curriculo));

            // TODO
            // Após alterar o currículo, é necessário iterar pelos elementos das listas e chamar suas respectivas classesDao para alterá-los

            formacaoDAO.Alterar(curriculo.Formacoes, ref curriculo);
            experienciaDAO.Alterar(curriculo.Experiencias, ref curriculo);
            idiomaDAO.Alterar(curriculo.Idiomas, ref curriculo);
        }

        /// <summary>
        /// Exclui um CurriculoViewModel na tabela dbo.tb_Curriculo
        /// </summary>
        /// <param name="curriculo">CurriculoViewModel que será excluído</param>
        public override void Excluir(CurriculoViewModel curriculo)
        {
            // TODO
            // Antes de excluir o currículo, é necessário iterar pelos elementos das listas e chamar suas respectivas classesDao para excluí-los
            // Esses métodos precisam apenas do ID da ViewModel
            formacaoDAO.Excluir(curriculo.Formacoes.Select(x => x.Id_Formacao).ToList());
            experienciaDAO.Excluir(curriculo.Experiencias.Select(x => x.Id_Experiencia).ToList());
            idiomaDAO.Excluir(curriculo.Idiomas.Select(x => x.Id_Idioma).ToList());

            string sql =
                $"DELETE FROM tb_Curriculo WHERE Id_Curriculo = {curriculo.Id_Curriculo};";

            HelperDAO.ExecutaSQL(sql);
        }

        /// <summary>
        /// Realiza a consulta de um elemento da tabela dbo.tb_Curriculo a partir de seu Id
        /// </summary>
        /// <param name="Id">Id do elemento que se está pesquisando</param>
        /// <returns>Se encontrar o registro, retorna-o, caso contrário retorna null</returns>
        public override CurriculoViewModel Consulta(int Id)
        {
            string sql = 
                $"SELECT * FROM tb_Curriculo WHERE Id_Curriculo = {Id};";

            DataTable tabela = HelperDAO.ExecutaSelect(sql);

            if (tabela.Rows.Count == 0)
                return null;

            return MontaModel(tabela.Rows[0]);
        }

        /// <summary>
        /// Retorna uma lista com todos os Curriculos cadastradas na tabela dbo.tb_Curriculo
        /// </summary>
        /// <returns>Lista com todas os Curriculos cadastrados ordenada pelo Id</returns>
        public override List<CurriculoViewModel> Listagem()
        {
            List<CurriculoViewModel> lista = new List<CurriculoViewModel>();

            string sql = $"SELECT * FROM tb_Curriculo ORDER BY Id_Curriculo;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

        /// <summary>
        /// Retorna uma lista com os Curriculos filtrados a partir do nome e do id da área
        /// </summary>
        /// <param name="Nome">Nome do dono do currículo</param>
        /// <param name="Id_Area">Id da área filtrada</param>
        /// <returns>Retorna uma lista com todos os curriculos que correspondem a uma dessas filtragens</returns>
        public List<CurriculoViewModel> Pesquisa(string Nome = null, int? Id_Area = null)
        {
            List<CurriculoViewModel> lista = new List<CurriculoViewModel>();

            string sql = $"SELECT * FROM tb_Curriculo WHERE 1 = 1 ";

            if (Nome != null)
                sql += "AND Nome LIKE @Nome + '%' ";

            if (Id_Area != null)
                sql += "AND Id_Area = @Id_Area ";

            sql += "ORDER BY Id_Curriculo;";

            DataTable tabela = HelperDAO.ExecutaSelect(sql, CriaParametros(Nome, Id_Area));

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

        /// <summary>
        /// Retorna uma lista com os Curriculos filtrados a partir do  id da area
        /// </summary>
        /// <param name="Id_Area">Id da Área que se deseja pesquisar</param>
        /// <returns>Retorna uma lista com todos os curriculos que correspondem a essa filtragem</returns>
        public List<CurriculoViewModel> Pesquisa(int? Id_Area)
        {
            return Pesquisa(null, Id_Area);
        }


        #endregion CRUD

    }
}

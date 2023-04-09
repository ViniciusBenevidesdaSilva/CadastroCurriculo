using CadastroCurriculo.DAO;
using CadastroCurriculo.Enums;
using CadastroCurriculo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CadastroCurriculo.Controllers
{
    public class HomeController : Controller
    {
        private static string Mensagem = null;

        private readonly ILogger<HomeController> _logger;

        #region DAOS

        AreaDAO areaDAO = new AreaDAO();
        CurriculoDAO curriculoDAO = new CurriculoDAO();

        #endregion DAOS


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        #region Métodos Index

        public IActionResult Index()
        {
            try
            {
                List<CurriculoViewModel> curriculos = curriculoDAO.Listagem();

                ViewBag.Areas = PreencheComboBoxArea();
                ViewBag.Mensagem = Mensagem;

                Mensagem = null;

                return View("Index", curriculos);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }

        private List<SelectListItem> PreencheComboBoxArea(int? IdSelecionado = null)
        {
            List<AreaViewModel> areas = areaDAO.Listagem();
            List<SelectListItem> listaAreas = new List<SelectListItem>();

            listaAreas.Add(new SelectListItem("Selecione...", "0"));

            foreach (var area in areas)
            {
                SelectListItem item = new SelectListItem(area.Desc_Area, area.Id_Area.ToString());
                listaAreas.Add(item);

                if (IdSelecionado != null && area.Id_Area == IdSelecionado)
                    item.Selected = true;
            }

            return listaAreas;
        }

        public IActionResult Pesquisa(string Nome = null, int? Id_Area = null)
        {
            try
            {
                if (Id_Area == 0)
                    Id_Area = null;

                List<CurriculoViewModel> curriculos = curriculoDAO.Pesquisa(Nome, Id_Area);

                ViewBag.Nome = Nome;
                ViewBag.Areas = PreencheComboBoxArea(Id_Area);

                return View("Index", curriculos);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }

        #endregion Métodos Index


        #region CRUD

        public IActionResult Create()
        {
            try
            {
                CurriculoViewModel curriculo = new CurriculoViewModel();
                curriculo.Id_Icone = 1;

                ViewBag.InfoCRUD = EnumInfoCRUD.Novo;
                ViewBag.ProficienciaAux = new SelectList(Enum.GetValues(typeof(EnumProficienciaIdioma)));

                ViewBag.Areas = PreencheComboBoxArea();
                ViewBag.Erro = false;
                ViewBag.Mensagem = Mensagem;

                Mensagem = null;

                /*
                while (curriculo.Experiencias.Count < 5)
                    curriculo.Experiencias.Add(new ExperienciaViewModel(curriculo));

                while (curriculo.Formacoes.Count < 3)
                    curriculo.Formacoes.Add(new FormacaoViewModel(curriculo));

                while (curriculo.Idiomas.Count < 3)
                    curriculo.Idiomas.Add(new IdiomaViewModel(curriculo));
                */

                return View("Form", curriculo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }

        public IActionResult Editar(int id)
        {
            try
            {
                CurriculoViewModel curriculo = curriculoDAO.Consulta(id);

                ViewBag.InfoCRUD = EnumInfoCRUD.Editar;
                ViewBag.ProficienciaAux = new SelectList(Enum.GetValues(typeof(EnumProficienciaIdioma)));
                ViewBag.Areas = PreencheComboBoxArea(curriculo.Area.Id_Area);
                ViewBag.Erro = false;

                ViewBag.Mensagem = Mensagem;
                Mensagem = null;

                /*
                while (curriculo.Experiencias.Count < 5)
                    curriculo.Experiencias.Add(new ExperienciaViewModel(curriculo));

                while (curriculo.Formacoes.Count < 3)
                    curriculo.Formacoes.Add(new FormacaoViewModel(curriculo));

                while (curriculo.Idiomas.Count < 3)
                    curriculo.Idiomas.Add(new IdiomaViewModel(curriculo));
                */

                return View("Form", curriculo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }

        public IActionResult Salvar(CurriculoViewModel curriculo, EnumInfoCRUD operacao)
        {
            try
            {
                ValidaCurriculo(curriculo);


                if (ModelState.IsValid == false)
                {
                    ViewBag.Erro = true;
                    ViewBag.Areas = PreencheComboBoxArea(curriculo.Area.Id_Area);
                    ViewBag.InfoCRUD = operacao;

                    ViewBag.ProficienciaAux = new SelectList(Enum.GetValues(typeof(EnumProficienciaIdioma)));

                    ViewBag.Mensagem = "Preencha os campos indicados!";

                    return View("Form", curriculo);
                }
                else
                {
                    CorrigeChavesEstrangeiras(ref curriculo);

                    if (curriculo.Id_Curriculo.HasValue && curriculoDAO.Consulta(curriculo.Id_Curriculo.Value) != null)
                        curriculoDAO.Alterar(curriculo);
                    else
                        curriculoDAO.Inserir(curriculo);

                }

                Mensagem = "Dados salvos com sucesso!";
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }

        private void ValidaCurriculo(CurriculoViewModel curriculo)
        {
            bool erro = false;

            ModelState.Clear();

            if (string.IsNullOrEmpty(curriculo.Nome))
            {
                ModelState.AddModelError("Nome", " *");
                erro = true;
            }

            if (string.IsNullOrEmpty(curriculo.CPF))
            {
                ModelState.AddModelError("CPF", " *");
                erro = true;
            }

            if (string.IsNullOrEmpty(curriculo.Cargo_Pretendido))
            {
                ModelState.AddModelError("Cargo_Pretendido", " *");
                erro = true;
            }

            if (curriculo.Id_Icone < 1 || curriculo.Id_Icone > 8)
            {
                ModelState.AddModelError("Id_Icone", " *");
                erro = true;
            }

            if (curriculo.Area == null || curriculo.Area.Id_Area < 1)
            {
                ModelState.AddModelError("Area.Id_Area", " *");
                erro = true;
            }

            if (erro)
                ModelState.AddModelError("Erro", "Campos obrigatórios");

        }


        private void CorrigeChavesEstrangeiras(ref CurriculoViewModel curriculo)
        {
            List<ExperienciaViewModel> experienciasValidas = new List<ExperienciaViewModel>();
            foreach (ExperienciaViewModel experiencia in curriculo.Experiencias)
            {
                if (!string.IsNullOrEmpty(experiencia.Desc_Experiencia))
                {
                    experiencia.Curriculo = curriculo;
                    experienciasValidas.Add(experiencia);
                }
            }

            curriculo.Experiencias = experienciasValidas;

            List<FormacaoViewModel> formacoesValidas = new List<FormacaoViewModel>();
            foreach (FormacaoViewModel formacao in curriculo.Formacoes)
            {
                if (!string.IsNullOrEmpty(formacao.Desc_Formacao))
                {
                    formacao.Curriculo = curriculo;
                    formacoesValidas.Add(formacao);
                }
            }

            curriculo.Formacoes = formacoesValidas;


            List<IdiomaViewModel> idiomasValidos = new List<IdiomaViewModel>();
            foreach (IdiomaViewModel idioma in curriculo.Idiomas)
            {
                if (!string.IsNullOrEmpty(idioma.Desc_Idioma))
                {
                    idioma.Curriculo = curriculo;
                    idiomasValidos.Add(idioma);
                }
            }

            curriculo.Idiomas = idiomasValidos;
        }


        public IActionResult Deletar(int Id_Curriculo)
        {
            try
            {
                CurriculoViewModel curriculo = curriculoDAO.Consulta(Id_Curriculo);

                if (curriculo != null)
                    curriculoDAO.Excluir(curriculo);

                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }


        #endregion CRUD


        public IActionResult Impressao(int id)
        {
            try
            {
                CurriculoViewModel curriculo = curriculoDAO.Consulta(id);
                
                if(curriculo == null)
                {
                    Mensagem = "Curriculo nao encontrado";
                    return RedirectToAction("index");
                }

                ViewBag.ExibirFooter = false;

                return View(curriculo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

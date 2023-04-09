using CadastroCurriculo.AbstractClasses;
using System.Collections.Generic;

namespace CadastroCurriculo.Models
{
    public class CurriculoViewModel : AbstractViewModel
	{
		//public override string ToString() => "Curriculo: " + Nome;


		/// <summary>
		/// Começa valendo null, uma vez que só será atribuído na inserção do banco
		/// </summary>
		public int? Id_Curriculo { get; set; } = null;

		public string Nome { get; set; }
		public string CPF { get; set; }
		public string Endereco { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }
		public string Cargo_Pretendido { get; set; }
		public int Id_Icone { get; set; }
		public double? Pretensao_Salarial { get; set; }

		public AreaViewModel Area { get; set; } = new AreaViewModel();

		public List<FormacaoViewModel> Formacoes { get; set; } = new List<FormacaoViewModel>();
		public List<ExperienciaViewModel> Experiencias { get; set; } = new List<ExperienciaViewModel>();
		public List<IdiomaViewModel> Idiomas { get; set; } = new List<IdiomaViewModel>();
	}
}

using CadastroCurriculo.AbstractClasses;

namespace CadastroCurriculo.Models
{
    public class ExperienciaViewModel : AbstractViewModel
    {
        /// <summary>
        /// Necessário informar à qual curriculo essa Experiência faz parte
        /// </summary>
        /// <param name="curriculoBase">Curriculo ao qual essa Experiência deve referenciar</param>
        public ExperienciaViewModel(CurriculoViewModel curriculoBase)
        {
            Curriculo = curriculoBase;
        }
        public ExperienciaViewModel()
        {
            Curriculo = null;
        }
        public override string ToString() => "Experiencia: " + Desc_Experiencia;


        public int? Id_Experiencia { get; set; } = null;
        public CurriculoViewModel Curriculo { get; set; }
        public string Desc_Experiencia { get; set; }
    }
}

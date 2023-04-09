using CadastroCurriculo.AbstractClasses;
using CadastroCurriculo.Enums;

namespace CadastroCurriculo.Models
{
    public class IdiomaViewModel : AbstractViewModel
    {
        /// <summary>
        /// Necessário informar à qual curriculo essa Idioma faz parte
        /// </summary>
        /// <param name="curriculoBase">Curriculo ao qual esse Idioma deve referenciar</param>
        public IdiomaViewModel(CurriculoViewModel curriculoBase)
        {
            Curriculo = curriculoBase;
        }

        public IdiomaViewModel()
        {
            Curriculo = null;
        }
        public override string ToString() => "Idioma: " + Desc_Idioma;


        public int? Id_Idioma { get; set; } = null;
        public CurriculoViewModel Curriculo { get; set; }
        public string Desc_Idioma { get; set; }
        public EnumProficienciaIdioma GrauProficiencia { get; set; }
    }
}

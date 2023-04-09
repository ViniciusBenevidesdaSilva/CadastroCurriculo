using CadastroCurriculo.AbstractClasses;

namespace CadastroCurriculo.Models
{
    public class FormacaoViewModel : AbstractViewModel
    {
        /// <summary>
        /// Necessário informar à qual curriculo essa Formação faz parte
        /// </summary>
        /// <param name="curriculoBase">Curriculo ao qual essa Formação deve referenciar</param>
        public FormacaoViewModel(CurriculoViewModel curriculoBase)
        {
            Curriculo = curriculoBase;
        }

        public FormacaoViewModel()
        {
            Curriculo = null;
        }

        public override string ToString() => "Formacao: " + Desc_Formacao;


        public int? Id_Formacao { get; set; }
        public CurriculoViewModel Curriculo { get; set; }
        public string Desc_Formacao { get; set; }
    }
}

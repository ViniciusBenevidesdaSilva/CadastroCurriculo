using CadastroCurriculo.AbstractClasses;

namespace CadastroCurriculo.Models
{
    public class AreaViewModel : AbstractViewModel
    {
        public override string ToString() => "Area: " + Desc_Area;


        public int Id_Area { get; set; }
        public string Desc_Area { get; set; }
    }
}

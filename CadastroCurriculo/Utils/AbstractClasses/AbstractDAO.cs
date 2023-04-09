using System.Collections.Generic;

namespace CadastroCurriculo.AbstractClasses
{
    public abstract class AbstractDAO<ViewModel> where ViewModel : AbstractViewModel
    {
        public abstract void Inserir(ViewModel viewModel);

        public abstract void Alterar(ViewModel viewModel);

        public abstract void Excluir(ViewModel viewModel);

        public abstract ViewModel Consulta(int Id);

        public abstract List<ViewModel> Listagem();
    }
}

using AuxiliaryFeature.GUI;
using AuxiliaryFeature.MVVM.Model;

namespace AuxiliaryFeature.MVVM.ViewModel
{
    public abstract class BaseViewModel<T> : ModelNotifyPropertyChanged
    {
        public abstract void OnClosedViewCommand();

        DelegateCommand closedViewCommand = null;
        public DelegateCommand ClosedViewCommand
        {
            get
            {
                if (closedViewCommand == null)
                    closedViewCommand = new DelegateCommand(OnClosedViewCommand);
                return closedViewCommand;
            }
        }
    }
}

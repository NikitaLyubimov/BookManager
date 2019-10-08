using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookManager.Mediator;

namespace BookManager.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentPageViewModel;
        public BaseViewModel CurrentPageViewModel { get { return _currentPageViewModel; }}

        private void ChangeViewModel(BaseViewModel viewModel)
        {
            SetProperty(ref _currentPageViewModel, viewModel);
        }

        private void GoToHelloScreen(object obj)
        {
            ChangeViewModel((BaseViewModel)obj);
        }

        public MainWindowViewModel()
        {
            MediatorMain.Subscribe("Navigate", GoToHelloScreen);
            MediatorMain.Notify("Navigate", new MainPageViewModel());
        }
    }
}

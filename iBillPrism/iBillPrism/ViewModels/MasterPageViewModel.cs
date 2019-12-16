using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iBillPrism.ViewModels
{
    public class MasterPageViewModel
    {
        INavigationService _navigationService;
        public DelegateCommand<string> MasterButtonClickCommand { get; set; }
        public MasterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MasterButtonClickCommand = new DelegateCommand<string>(MasterNavigate);
        }

        async void MasterNavigate(string page)
        {
            await _navigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}

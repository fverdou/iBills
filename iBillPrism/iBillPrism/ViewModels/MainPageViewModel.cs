using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iBillPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand ButtonClickCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            ButtonClickCommand = new DelegateCommand(ShowCalendar);
        }

        async void ShowCalendar()
        {
            await NavigationService.NavigateAsync("CalendarPage");
        }
    }
}

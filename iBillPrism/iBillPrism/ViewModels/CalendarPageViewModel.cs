using iBillPrism.Contracts;
using iBillPrism.Models;
using MvvmHelpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iBillPrism.ViewModels
{
    public class CalendarPageViewModel : ViewModelBase
    {

        public DelegateCommand ButtonAddBillClicked { get; }
        public DelegateCommand<Bill> ItemTappedCommand { get; }

        public ObservableRangeCollection<Bill> ListOfBills { get; } 

        public CalendarPageViewModel(INavigationService navigationService, IRepository repository)
            : base(navigationService)
        {
            _repository = repository;
            ButtonAddBillClicked = new DelegateCommand(AddBillCommand);
            ItemTappedCommand = new DelegateCommand<Bill>(o => ListViewTap((Bill)o));

            ListOfBills = new ObservableRangeCollection<Bill>();
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var data = await _repository.GetAllBills();
            ListOfBills.ReplaceRange(data.OrderBy(x => x.DueDate));
        }

        async void AddBillCommand()
        {         
            await NavigationService.NavigateAsync("DataEntryPage");
        }

        async void ListViewTap(Bill b)
        {
            var parameters = new NavigationParameters();
            parameters.Add("bill", b);
            //new navigationparameters { { "bill", b } }
            await NavigationService.NavigateAsync("DataEntryPage", parameters);

            //var action = await _pageDialogService.DisplayActionSheetAsync("", "Cancel", null, "Edit bill", "Delete bill");

            //switch (action)
            //{
            //    case "Edit bill":
            //        {
            //            var parameters = new NavigationParameters();
            //            parameters.Add("bill", b);
            //            //new NavigationParameters { { "bill", b } }
            //            await NavigationService.NavigateAsync("DataEntryPage", parameters);
            //        }
            //        break;
            //    case "Delete bill":

            //        await _repository.Remove(b); 
            //        var data = await _repository.GetAll();
            //        ListOfBills.ReplaceRange(data.OrderBy(x => x.DueDate));

            //        break;
            //}

        }

        private readonly IRepository _repository;
    }
}

using iBillPrism.Contracts;
using iBillPrism.Models;
using MvvmHelpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iBillPrism.ViewModels
{
    public class CalendarPageViewModel : ViewModelBase
    {

        //public DelegateCommand ButtonAddBillClicked { get; }
        public DelegateCommand<Bill> EditButtonCommand { get; }
        public DelegateCommand<Bill> DeleteButtonCommand { get; }
        //public ObservableRangeCollection<Bill> ListOfBills { get; }
        public ObservableRangeCollection<BillsGroup> ListOfBills { get; }

        public CalendarPageViewModel(INavigationService navigationService, IRepository repository, IPageDialogService dialogService)
            : base(navigationService)
        {
            _pageDialogService = dialogService;
            _repository = repository;
            //ButtonAddBillClicked = new DelegateCommand(AddBillCommand);
            EditButtonCommand = new DelegateCommand<Bill>(o => EditButtonTap(o));
            DeleteButtonCommand = new DelegateCommand<Bill>(o => DeleteButtonTap(o));

            //ListOfBills = new ObservableRangeCollection<Bill>();
            ListOfBills = new ObservableRangeCollection<BillsGroup>();
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var data = await _repository.GetAllBills();
            //ListOfBills.ReplaceRange(data.OrderBy(x => x.DueDate));
            ListOfBills.ReplaceRange(
                data
                .GroupBy(x => x.DueDate)
                .OrderBy(x => x.Key)
                .Select(x => new BillsGroup(x.Key, x.ToList())));
        }

        private async void DeleteButtonTap(Bill b)
        {
            bool answer = await _pageDialogService.DisplayAlertAsync(null, "Are you sure you want to delete this bill?", "Yes", "No");
            if (answer)
            {
                await _repository.RemoveBill(b);
                var data = await _repository.GetAllBills();
                //ListOfBills.ReplaceRange(data.OrderBy(x => x.DueDate));
                ListOfBills.ReplaceRange(
                data
                .GroupBy(x => x.DueDate)
                .OrderBy(x => x.Key)
                .Select(x => new BillsGroup(x.Key, x.ToList())));
            }
        }

        //async void AddBillCommand()
        //{         
        //    await NavigationService.NavigateAsync("DataEntryPage");
        //}

        async void EditButtonTap(Bill b)
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
        private readonly IPageDialogService _pageDialogService;
    }
}

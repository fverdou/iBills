using iBillPrism.Contracts;
using iBillPrism.Models;
using MvvmHelpers;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iBillPrism.ViewModels
{
    public class CalendarPaidViewModel : ViewModelBase, IActiveAware
    {
        public event EventHandler IsActiveChanged;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }

        private void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
            RefreshData();
        }
        private bool _isActive;
        public DelegateCommand<Bill> EditButtonCommand { get; }
        public DelegateCommand<Bill> DeleteButtonCommand { get; }
        public ObservableRangeCollection<BillsGroup> ListOfBills { get; }
        public CalendarPaidViewModel(INavigationService navigationService, IRepository repository, IPageDialogService dialogService)
            : base(navigationService)
        {
            _pageDialogService = dialogService;
            _repository = repository;
            EditButtonCommand = new DelegateCommand<Bill>(o => EditButtonTap(o));
            DeleteButtonCommand = new DelegateCommand<Bill>(o => DeleteButtonTap(o));

            ListOfBills = new ObservableRangeCollection<BillsGroup>();
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            RefreshData();
        }
        private async void RefreshData()
        {
            var data = await _repository.GetAllBills();
            data = data.Where(x => x.PayDate != null);
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
                data = data.Where(x => x.PayDate != null);
                ListOfBills.ReplaceRange(
                data
                .GroupBy(x => x.DueDate)
                .OrderBy(x => x.Key)
                .Select(x => new BillsGroup(x.Key, x.ToList())));
            }
        }

        async void EditButtonTap(Bill b)
        {
            var parameters = new NavigationParameters();
            parameters.Add("bill", b);
            await NavigationService.NavigateAsync("DataEntryPage", parameters);

        }
        private readonly IRepository _repository;
        private readonly IPageDialogService _pageDialogService;
    }
}

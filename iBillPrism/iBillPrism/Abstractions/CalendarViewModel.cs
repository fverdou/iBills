using iBillPrism.Contracts;
using iBillPrism.Models;
using iBillPrism.ViewModels;
using MvvmHelpers;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBillPrism.Abstractions
{
    public class CalendarViewModel : ViewModelBase, IActiveAware
    {
        protected Func<Bill, bool> predicate;
        public event EventHandler IsActiveChanged;
        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand<Bill> EditButtonCommand { get; }
        public DelegateCommand<Bill> DeleteButtonCommand { get; }
        public ObservableRangeCollection<BillsGroup> ListOfBills { get; }
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }
        private void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
            RefreshCommand.Execute();
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        public CalendarViewModel(INavigationService navigationService, IRepository repository, IPageDialogService dialogService)
            : base(navigationService)
        {
            _pageDialogService = dialogService;
            _repository = repository;
            EditButtonCommand = new DelegateCommand<Bill>(async o => await EditButtonTap(o));
            DeleteButtonCommand = new DelegateCommand<Bill>(async o => await DeleteButtonTap(o));
            RefreshCommand = new DelegateCommand(async () => await RefreshData());

            ListOfBills = new ObservableRangeCollection<BillsGroup>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            RefreshCommand.Execute();
        }
        private async Task RefreshData()
        {
            try
            {
                var data = await _repository.GetAllBills();
                data = data.Where(predicate);
                ListOfBills.ReplaceRange(
                data
                .GroupBy(x => x.DueDate)
                .OrderBy(x => x.Key)
                .Select(x => new BillsGroup(x.Key, x.ToList())));
            }
            catch (Exception exception)
            {
                await _pageDialogService.DisplayAlertAsync("Error !!", exception.Message, "Ok");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task DeleteButtonTap(Bill b)
        {
            bool answer = await _pageDialogService.DisplayAlertAsync(null, "Are you sure you want to delete this bill?", "Yes", "No");
            if (answer)
            {
                await _repository.RemoveBill(b);
                await RefreshData();
            }
        }

        async Task EditButtonTap(Bill b)
        {
            var parameters = new NavigationParameters();
            parameters.Add("bill", b);
            await NavigationService.NavigateAsync("DataEntryPage", parameters);

        }
        private readonly IRepository _repository;
        private readonly IPageDialogService _pageDialogService;
        private bool _isActive;
        private bool _isRefreshing = false;
        private INavigationService navigationService;
    }

    public abstract class Point
    {
        int x;
        int y;

        public Point(int X, int Y)
        {

        }
    }

    public class CartesianPoint : Point
    {
        public CartesianPoint(int X, int Y)
            :base(X,Y)
        {

        }
    }
}
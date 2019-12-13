using iBillPrism.Contracts;
using iBillPrism.Models;
using MvvmHelpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;

namespace iBillPrism.ViewModels
{
    public class DataEntryPageViewModel : ViewModelBase
    {
        public List<string> BillTypes { get; set; } = new List<string>
        {
            "Energy Bill", "Gas Bill", "Telephone Bill", "Cellphone Bill", "Loan bill"
        };
        public string SelectedBillType
        {
            get => _selectedBillType;
            set => SetProperty(ref _selectedBillType, value, () => PickerTypeSelected());
        }
        public string BillAmount
        {
            get => _billAmount;
            set => SetProperty(ref _billAmount, value, () => EntryAmountChanged());
        }
        public bool LabelAlertVisible
        {
            get => _labelAlertVisible;
            set => SetProperty(ref _labelAlertVisible, value);
        }
        public string LabelAlertText
        {
            get => _labelAlertText;
            set => SetProperty(ref _labelAlertText, value);
        }
        public bool ButtonOkEnabled
        {
            get => _buttonOkEnabled;
            set => SetProperty(ref _buttonOkEnabled, value);
        }
        public bool ButtonDeleteEnabled
        {
            get => _buttonDeleteEnabled;
            set => SetProperty(ref _buttonDeleteEnabled, value);
        }
        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }
        public DateTime? SelectedPayDate
        {
            get => _selectedPayDate;
            set => SetProperty(ref _selectedPayDate, value);
        }
        public DateTime SelectedDueDate
        { 
            get => _selectedDueDate; 
            set => SetProperty(ref _selectedDueDate, value); 
        }
        public DelegateCommand ButtonOkClickCommand { get; }
        public DelegateCommand ButtonDeleteClickCommand { get; }
        public DelegateCommand ButtonCancelClickCommand { get; }

        public DataEntryPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRepository repository)
            : base(navigationService)
        {
            _pageDialogService = dialogService;
            _repository = repository;

            ButtonText = "Add Bill";
            SelectedDueDate = DateTime.Today;
            SelectedPayDate = null;
            ButtonOkClickCommand = new DelegateCommand(BillOk);
            ButtonDeleteClickCommand = new DelegateCommand(DeleteBill);
            ButtonCancelClickCommand = new DelegateCommand(BillCancel);
            ButtonDeleteEnabled = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            // _bill = parameters["bill"] as Bill;
            // if(parameters["bill"] is Bill b) { b.Amount}
            if (parameters.ContainsKey("bill"))
            {
                _bill = parameters.GetValue<Bill>("bill");
                ButtonText = "Edit Bill";

                ButtonDeleteEnabled = true;

                SelectedBillType = _bill.Type;
                BillAmount = _bill.Amount.ToString();
                SelectedDueDate = _bill.DueDate;
                SelectedPayDate = _bill.PayDate;
            }

        }

        void PickerTypeSelected()
        {
            pickerTypeSelected = true;
            ButtonNewBillEnable();
        }
        void EntryAmountChanged()
        {
            try
            {
                decimal amount = ValidateExtractEntryAmount();
                entryAmountChanged = true;
            }
            catch (Exception ex)
            {
                entryAmountChanged = false;
                LabelAlertVisible = true;
                LabelAlertText = ex.Message;
            }
            ButtonNewBillEnable();
        }
        void ButtonNewBillEnable()
        {
            if (pickerTypeSelected && entryAmountChanged)
            {
                ButtonOkEnabled = true;
                LabelAlertVisible = false;
            }
            else
            {
                ButtonOkEnabled = false;
            }
        }
        async void BillOk()
        {
            if (SelectedBillType == null)
            {
                await _pageDialogService.DisplayAlertAsync("", "The bill type can't be empty!", "OK");
                return;
            }
            decimal amount = 0;
            try
            {
                amount = ValidateExtractEntryAmount();
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("", ex.Message, "OK");
                return;
            }
            
            _bill ??= new Bill();

            _bill.Type = SelectedBillType;
            _bill.Amount = amount;
            _bill.DueDate = SelectedDueDate.AddMinutes(1439);
            _bill.PayDate = SelectedPayDate;

            if (_bill.Id == 0)
            {
                await _repository.Add(_bill);
            }
            else
            {
                await _repository.Update(_bill);
            }
            
            await NavigationService.GoBackAsync();
        }
        async void DeleteBill()
        {
            bool answer = await _pageDialogService.DisplayAlertAsync(null, "Are you sure you want to delete this bill?", "Yes", "No");
            if (answer)
            {
                await _repository.Remove(_bill);
                //var data = await _repository.GetAll();
                //ListOfBills.ReplaceRange(data.OrderBy(x => x.DueDate));
                await NavigationService.GoBackAsync();
            }
        }
        async void BillCancel()
        {
            await NavigationService.GoBackAsync();
        }

        private string _selectedBillType;
        private string _billAmount;
        private bool pickerTypeSelected = false;
        private bool entryAmountChanged = false;
        private bool _labelAlertVisible;
        private string _labelAlertText;
        private bool _buttonOkEnabled;
        private bool _buttonDeleteEnabled;
        private string _buttonText;
        private decimal ValidateExtractEntryAmount()
        {
            if (string.IsNullOrWhiteSpace(BillAmount))
            {
                throw new Exception("The due amount can't be empty!");
            }
            if (!decimal.TryParse(BillAmount, out decimal amount))
            {
                throw new Exception("The due amount must be a number!");
            }
            if (amount < 0)
            {
                throw new Exception("The due amount can't be negative!");
            }
            return amount;
        }
        private readonly IPageDialogService _pageDialogService;
        private readonly IRepository _repository;
        private Bill _bill;
        private DateTime? _selectedPayDate;
        private DateTime _selectedDueDate;
    }
}

using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using iBillPrism.Contracts;
using iBillPrism.Models;
using MvvmHelpers;
using iBillPrism.Abstractions;

namespace iBillPrism.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public Page Page { get; set; }

        public DelegateCommand AddCustomBillTypeCommand { get; }
        public ObservableRangeCollection<BillType> ListOfBillTypes { get; }
        public DelegateCommand<BillType> DeleteButtonCommand { get; }
        public DelegateCommand<BillType> EditButtonCommand { get; }
        public bool EditButtonVisible
        {
            get => _editButtonVisible;
            set => SetProperty(ref _editButtonVisible, value);
        }
        public bool DeleteButtonVisible
        {
            get => _deleteButtonVisible;
            set => SetProperty(ref _deleteButtonVisible, value);
        }
        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRepository repository)
            :base(navigationService)
        {
            _pageDialogService = dialogService;
            _repository = repository;
            AddCustomBillTypeCommand = new DelegateCommand(AddCustomBillType);
            ListOfBillTypes = new ObservableRangeCollection<BillType>();
            DeleteButtonCommand = new DelegateCommand<BillType>(o => DeleteButtonTap((BillType)o));
            EditButtonCommand = new DelegateCommand<BillType>(o => EditButtonTap((BillType)o));
        }

        async private void EditButtonTap(BillType o)
        {
            string customBillType = await Page.DisplayPromptAsync("Type Custom Bill Type", "");
            if (customBillType == null)
            {
                return;
            }
            else if (!string.IsNullOrWhiteSpace(customBillType))
            {
                o.Description = customBillType;
                await _repository.UpdateBillType(o);
                UpdateList();
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("", "The bill type can't be empty!", "OK");
            }
        }

        async private void AddCustomBillType()
        {
            string customBillType = await Page.DisplayPromptAsync("Type Custom Bill Type", "");
            if (customBillType == null)
            {
                return;
            }
            else if (!string.IsNullOrWhiteSpace(customBillType))
            {
                BillType billtype = new BillType
                {
                    Description = customBillType,
                    IsCustom = true
                };
                await _repository.AddBillType(billtype);
                UpdateList();
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("", "The bill type can't be empty!", "OK");
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            UpdateList();
        }
        async void DeleteButtonTap(BillType b)
        {
            bool answer = await _pageDialogService.DisplayAlertAsync(null, "Are you sure you want to delete this bill type?", "Yes", "No");
            if (answer)
            {
                //if (b.IsCustom)
                //{
                    await _repository.RemoveBillType(b);
                    UpdateList();
                //}
                //else
                //{
                //    await _pageDialogService.DisplayAlertAsync("", "You can't delete a default bill type!", "OK");
                //}
            }
        }
        async private void UpdateList()
        {
            var data = await _repository.GetAllBillTypes();
            ListOfBillTypes.ReplaceRange(data.OrderBy(x => x.Description));
        }

        private IPageDialogService _pageDialogService;
        private bool _editButtonVisible;
        private bool _deleteButtonVisible;
        private readonly IRepository _repository;
    }
}

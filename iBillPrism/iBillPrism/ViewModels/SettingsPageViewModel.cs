﻿using Prism.Commands;
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
        public DelegateCommand<BillType> ItemTappedCommand { get; }
        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IRepository repository)
            :base(navigationService)
        {
            _pageDialogService = dialogService;
            _repository = repository;
            AddCustomBillTypeCommand = new DelegateCommand(AddCustomBillType);
            ListOfBillTypes = new ObservableRangeCollection<BillType>();
            ItemTappedCommand = new DelegateCommand<BillType>(o => ListViewTap((BillType)o));
        }
        async private void AddCustomBillType()
        {
            string customBillType = await Page.DisplayPromptAsync("Type Custom Bill Type", "");
            //var _billType = new BillType
            //{
            //    Type = customBillType
            //};
            //await _repository.Add(_billType);
            if (!string.IsNullOrWhiteSpace(customBillType))
            {
                await _repository.Add(new BillType
                {
                    Type = customBillType,
                    IsCustom = true
                });
                await NavigationService.NavigateAsync("SettingsPage");
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("", "The bill type can't be empty!", "OK");
            }
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var data = await _repository.GetAll<BillType>();
            ListOfBillTypes.ReplaceRange(data.OrderBy(x => x.Type));
        }
        async void ListViewTap(BillType b)
        {
            bool answer = await _pageDialogService.DisplayAlertAsync(null, "Are you sure you want to delete this bill type?", "Yes", "No");
            if (answer)
            {
                await _repository.Remove(b);
                await NavigationService.NavigateAsync("SettingsPage");
            }
        }

        private IPageDialogService _pageDialogService;
        private readonly IRepository _repository;
    }
}
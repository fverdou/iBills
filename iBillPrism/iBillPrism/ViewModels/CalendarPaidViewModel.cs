﻿using iBillPrism.Abstractions;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iBillPrism.ViewModels
{
    public class CalendarPaidViewModel : CalendarViewModel
    {
        public CalendarPaidViewModel(INavigationService navigationService, IRepository repository, IPageDialogService dialogService)
            : base(navigationService, repository, dialogService)
        {
            predicate = x => x.PayDate != null;
        }
    }
}
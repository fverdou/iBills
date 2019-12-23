using Prism;
using Prism.Ioc;
using iBillPrism.ViewModels;
using iBillPrism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using iBillPrism.Models;
using System;
using iBillPrism.Contracts;
using iBillPrism.Services;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iBillPrism
{
    public partial class App
    {
        public List<string> BillTypes { get; set; } = new List<string>
        {
            "Energy Bill", "Gas Bill", "Telephone Bill", "Cellphone Bill", "Loan bill"
        };
         /* The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            await NavigationService.NavigateAsync("/MasterPage/NavigationPage/TabbedPage");            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {         
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bills.db3");
            var repo = new DbRepository(dbPath);

            containerRegistry.RegisterInstance<IRepository>(repo);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CalendarPage, CalendarPageViewModel>();            
            containerRegistry.RegisterForNavigation<DataEntryPage, DataEntryPageViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<Views.TabbedPage, TabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<CalendarPending, CalendarPendingViewModel>();
            containerRegistry.RegisterForNavigation<CalendarPaid, CalendarPaidViewModel>();
            containerRegistry.RegisterForNavigation<CalendarOverdue, CalendarOverdueViewModel>();
        }
    }
}

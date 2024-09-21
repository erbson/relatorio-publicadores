using App.Relatorio.ViewModels;
using App.Relatorio.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;

namespace App.Relatorio
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
     
        public AppShell()
        {
            InitializeComponent();
           Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(AnotacoesPage), typeof(AnotacoesPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

      

    }
}

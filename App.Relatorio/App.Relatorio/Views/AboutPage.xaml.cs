using App.Relatorio.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Relatorio.Views
{
    public partial class AboutPage : ContentPage
    {
      
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }


    }
}
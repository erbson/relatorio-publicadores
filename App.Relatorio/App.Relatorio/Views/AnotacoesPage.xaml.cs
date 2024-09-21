using App.Relatorio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Relatorio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnotacoesPage : ContentPage
    {
        AnotacoesViewModel _viewModel;
        public AnotacoesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel =  new AnotacoesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
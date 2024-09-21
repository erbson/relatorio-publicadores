using App.Relatorio.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App.Relatorio.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
using App.Relatorio.Models;
using App.Relatorio.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Relatorio.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage(TimeSpan horas,bool Salvar)
        {
         
            if (horas == TimeSpan.Zero)   
              horas = new TimeSpan(0, 0, 0);

            InitializeComponent();
            BindingContext = new NewItemViewModel(horas,Salvar);
        }

        public NewItemPage()
        {
            TimeSpan horas = new TimeSpan(0, 0, 0);
            InitializeComponent();
          
            BindingContext = new NewItemViewModel(horas,false);
        }

     
    }
}
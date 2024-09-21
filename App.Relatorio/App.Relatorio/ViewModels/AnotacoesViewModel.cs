using App.Agenda.DataBaseHelper;
using App.Relatorio.Models;
using App.Relatorio.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App.Relatorio.ViewModels
{
    public class AnotacoesViewModel : BaseViewModel
    {
        private Models.Relatorio _selectedItem;

        DataBase bd = new DataBase();
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Models.AnotacoesDoMEs> ItemRelatorios { get; }
        public List<Models.AnotacoesDoMEs> itemRelatorio { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Models.AnotacoesDoMEs> ItemTapped { get; }
        public Command Selecao { get; }


        public AnotacoesViewModel()
        {
            Title = "Relatorio de campo";
            Items = new ObservableCollection<Item>();
            ItemRelatorios = new ObservableCollection<Models.AnotacoesDoMEs>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
     


        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

          
            try
            {
              
                ItemRelatorios.Clear();
                itemRelatorio = bd.GetAnotacoes();

                if(itemRelatorio.Any())
                    itemRelatorio.ForEach(x => x.MesExtenso = "Campo do dia " + x.MesAno) ;

              
                foreach (var item in itemRelatorio)
                {
                   
                    ItemRelatorios.Add(item) ;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
      
      
        }





    }
}
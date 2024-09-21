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
    public class ItemsViewModel : BaseViewModel
    {
        private Models.Relatorio _selectedItem;

        DataBase bd = new DataBase();
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Models.Relatorio> ItemRelatorios { get; }
        public List<Models.Relatorio> itemRelatorio { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Models.Relatorio> ItemTapped { get; }
        public Command Selecao { get; }


        public ItemsViewModel()
        {
            Title = "Inicio";
            Items = new ObservableCollection<Item>();
            ItemRelatorios = new ObservableCollection<Models.Relatorio>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Models.Relatorio>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

          
            try
            {
                //Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                // Items.Add(item);
                //}
                ItemRelatorios.Clear();
                itemRelatorio = bd.GetRelatorios().Where(x => x.indentificadorFirebase != null).ToList() ;

                itemRelatorio.ForEach(x => x.MesExtenso = "Relatorio do mês de " + Models.Relatorio.ExibirMesPorExtenso(x.MesDeServico.Date.ToString())  + " enviado em " + x.MesAno) ;
              
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
            SelectedItem = null;
        }

        public Models.Relatorio SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnDateSelected(object sender, DateChangedEventArgs args)
        {

        }

       async void OnItemSelected(Models.Relatorio item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
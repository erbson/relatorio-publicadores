using App.Agenda.DataBaseHelper;
using App.Relatorio.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.Relatorio.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string txtnome;

        private string txthoras;

        private int txtrevisitas;
        private int txtpublicacoes;
        private int txtvideos;
        private int txtestudos;
        private string txtMes;
        private string txtobservacoes;

        DataBase bd = new DataBase();
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string TxtNome
        {
            get => txtnome;
            set => SetProperty(ref txtnome, value);
        }
        public string TxtHoras
        {
            get => txthoras;
            set => SetProperty(ref txthoras, value);
        }

        public int TxtRevisitas
        {
            get => txtrevisitas;
            set => SetProperty(ref txtrevisitas, value);
        }

        public int TxtPublicacoes
        {
            get => txtpublicacoes;
            set => SetProperty(ref txtpublicacoes, value);
        }

        public string TxtMes
        {
            get => txtMes;
            set => SetProperty(ref txtMes, value);
        }
        public int TxtVideos
        {
            get => txtvideos;
            set => SetProperty(ref txtvideos, value);
        }

        public int TxtEstudos
        {
            get => txtestudos;
            set => SetProperty(ref txtestudos, value);
        }

        public string TxtObservacoes
        {
            get => txtobservacoes;
            set => SetProperty(ref txtobservacoes, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        DateTime _dateTo;
        public DateTime MesDeServico
        {
            set { SetProperty(ref _dateTo, value); }
            get { return _dateTo; }
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                Models.Relatorio relatorio = new Models.Relatorio();

                //var item = await DataStore.GetItemAsync(itemId);
                //Id = item.Id;
                //Text = item.Text;
                //Description = item.Description;
                relatorio = bd.GetRelatorio(Convert.ToInt32(ItemId));
                TxtNome = relatorio.NomePublicador;
                TxtHoras = relatorio.Horas.ToString();
                TxtVideos = relatorio.Videos;
                TxtEstudos = relatorio.Estudos;
                TxtObservacoes = relatorio.Observacao;
                TxtRevisitas = relatorio.Revisitas;
                TxtPublicacoes = relatorio.Publicacoes;
                TxtMes = Models.Relatorio.ExibirMesPorExtenso(relatorio.MesDeServico.Date.ToString());




            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void LoadItemId2(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}

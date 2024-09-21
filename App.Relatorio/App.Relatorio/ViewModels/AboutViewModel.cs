using App.Relatorio.Views;
using Plugin.Connectivity;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace App.Relatorio.ViewModels
{
    public class AboutViewModel : BaseViewModel,  INotifyPropertyChanged
    {
        private string resultado;
        public event PropertyChangedEventHandler PropertyChanged;
        private Boolean btInicioar = true;
        private Boolean btparar = true;
        Timer timer;
        public Command AddItemCommand { get; }
        public Command BotaoIniciar { get; }
        public Command BotaoParar { get; }

        int hours = 0, mins = 0, secs = 0, milliseconds = 0;

        Stopwatch stopWatch = new Stopwatch();
        public AboutViewModel()
        {
            Title = "Inicio";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            AddItemCommand = new Command(OnAddItem);
            BotaoIniciar = new Command(Iniciar);
            BotaoParar = new Command(Parar);
        }

        public string Resultado
        {

            get => resultado;
            set => SetProperty(ref resultado, value);
        }

        public Boolean BtIniciar
        {
            get
            {
                return btInicioar;
            }
            set
            {
                btInicioar = value;
                RaisePropertyChanged();
            }
        }


        public Boolean BtParar
        {
            get
            {
                return btparar;
            }
            set
            {
                btparar = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private async void OnAddItem(object obj)

        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await Shell.Current.GoToAsync(nameof(NewItemPage));
            }
            else
            {
                await Shell.Current.Navigation.PushAsync(new InternetPage());
               
            }

          


          
        }

        private async void Iniciar(object obj)
        {
            timer = new Timer();
            timer.Interval = 1; // 1 milliseconds  
            timer.Elapsed += Timer_Elapsed; ;
            timer.Start();
        }

        private void  Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            milliseconds++;
            if (milliseconds >= 1000)
            {
                secs++;
                milliseconds = 0;
            }
            if (secs == 59)
            {
                mins++;
                secs = 0;

            }
            if (mins == 59)
            {
                hours++;
                mins = 0;
            }



            Device.BeginInvokeOnMainThread(() =>
            {
                Resultado = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, mins, secs, milliseconds / 10);
            });
        }


        private void Parar(object obj)
        {
            milliseconds++;
            if (milliseconds >= 1000)
            {
                secs++;
                milliseconds = 0;
            }
            if (secs == 59)
            {
                mins++;
                secs = 0;

            }
            if (mins == 59)
            {
                hours++;
                mins = 0;
            }

       
            Device.BeginInvokeOnMainThread(() =>
            {
                Resultado = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, mins, secs, milliseconds / 10);
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}
using Android.Content.Res;
using App.Agenda.DataBaseHelper;
using App.Relatorio.Models;
using App.Relatorio.Services;
using App.Relatorio.Views;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Relatorio
{
    public partial class App : Application
    {
        DataBase db = new DataBase();
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<ViewModels.IMessageService, ViewModels.MessageService>();
            db.CriarBancoDeDados();
            if (string.IsNullOrEmpty(db.GetPublicador()))
            {
                MainPage = new LoginInicialPage();
            }
            else
            {
                MainPage = new AppShell();
            }
           
        }

        protected override  void OnStart()
        {
            var relogio = db.GetCronometro().LastOrDefault();
            if (relogio != null)
            {
                if (relogio.Status == 0 && Convert.ToDateTime(relogio.Dia) == DateTime.Today)
                {

                   // MainPage = new AppShell();
                   Shell.Current.Navigation.PushAsync(new RelogioPage());
                }
            }

        }

        protected override void OnSleep()
        {

            // Shell.Current.Navigation.PopToRootAsync();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //Shell.Current.GoToAsync("..");
            //Shell.Current.GoToAsync("///AboutPage");

        }

        protected override void OnResume()
        {
            var relogio = db.GetCronometro().LastOrDefault();
            if (relogio != null)
            {
                if (relogio.Status == 0 && Convert.ToDateTime(relogio.Dia) == DateTime.Today)
                {
                    Shell.Current.Navigation.PushAsync(new RelogioPage());
                }
            }

                    //Shell.Current.GoToAsync("///RelogioPage");
                   
        }
    }
}

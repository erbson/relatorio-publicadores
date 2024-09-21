using Android.App;
using Android.OS;
using App.Agenda.DataBaseHelper;
using App.Relatorio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Java.Util.Jar.Attributes;
using Debug = System.Diagnostics.Debug;

namespace App.Relatorio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class RelogioPage : ContentPage
    {
       
        int hours = 0, mins = 0, secs = 0, milliseconds = 0;
        int _horas = 0, _minutos = 0, _segundos = 0, _milesegundos = 0;
        bool EmOperacao = false;
        TimeSpan NovasHoras = new TimeSpan();
        DataBase db = new DataBase();
        private string _resultado;
        private bool IsRefreshing { get; set; }
        System.Timers.Timer timer = new System.Timers.Timer();

        public string Resultado 
        {
            get { return _resultado; }   // get method
            set { _resultado = value; }  // set method
        }
       

        public Command CarregarCommand { get; }
        Stopwatch cronmetro = new Stopwatch();
        public Command LoadItemsCommand { get; }
      
        public RelogioPage()
        {
           
            var relogio = db.GetCronometro().LastOrDefault();
            TimeSpan HorasAtuais = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TimeSpan TotaisHoras = new TimeSpan();
            if (relogio != null)
            {
                if (relogio.Status == 0 && Convert.ToDateTime(relogio.Dia) == DateTime.Today)
                {
                    InitializeComponent();
                  
                      EmOperacao = true;
                    TotaisHoras = HorasAtuais.Subtract(relogio.horas);
                    _horas = TotaisHoras.Hours;
                    _minutos = TotaisHoras.Minutes;
                    _segundos = TotaisHoras.Seconds;
                    _milesegundos = TotaisHoras.Milliseconds;
                 

                    timer.Interval = 1; // 1 milliseconds  
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();
                    cronmetro.Start();
                    TimeSpan ts = cronmetro.Elapsed;

                    /// lbl_result.Text = string.Format("{0:hh\\:mm\\:ss\\:fff}", cronmetro.Elapsed);

                    lbl_result.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", _horas, _minutos, _segundos, _milesegundos / 10);

                }
                else
                {
                    InitializeComponent();
                    lbl_result.Text = "00:00:000";

                }
            }
            else
            {
                InitializeComponent();
                lbl_result.Text = "00:00:000";
            }

         

        }


        //System.Timers.Timer timer = new System.Timers.Timer();
        private async void LancarRelatorio_Clicked(object sender, EventArgs e)
        {

            
           await Navigation.PushAsync(new NewItemPage(TimeSpan.Parse(lbl_result.Text),true));
            timer.Dispose();
            lbl_result.Text = "00:00:000";
        }

        private void btn_Stop_Clicked(object sender, EventArgs e)
        {
            timer.Stop();

            TimeSpan HorasAtuais = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Relogio cronometro_ = new Relogio();
            cronometro_.Status = 1;
            cronometro_.Dia = DateTime.Now.ToString("yyyy-MM-dd");
            cronometro_.pausa = HorasAtuais;
            var relogio = db.GetCronometro().LastOrDefault();
            if (relogio !=null && relogio.Status == 0)
            {
                db.UpdateCronometro(cronometro_);
            }


        }
        private void btn_Reset_Clicked(object sender, EventArgs e)
        {
            timer.Dispose();
            var relogio = db.GetCronometro().LastOrDefault();
            if(relogio != null && Convert.ToDateTime(relogio.Dia)== DateTime.Today)  
            {
                
                 db.DeletarCronometro(relogio);
        
                
            }
            _horas = 0;
            _minutos = 0;
            _segundos = 0;
            _milesegundos = 0;

            lbl_result.Text = "00:00:000";
        }

        private void btn_Start_Clicked(object sender, EventArgs e)
        {
            
            TimeSpan HorasAtuais = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TimeSpan TotaisHoras = new TimeSpan();
            Relogio cronometro_ = new Relogio();
            cronometro_.Status = 0;
            cronometro_.Dia = DateTime.Now.ToString("yyyy-MM-dd");
            cronometro_.horas = HorasAtuais;
            var relogio = db.GetCronometro().LastOrDefault();
            if (relogio == null ||  Convert.ToDateTime(relogio.Dia) < DateTime.Today)
            {
                db.InserirRelogio(cronometro_);
            }else if (relogio != null && relogio.Status == 1)
            {
                EmOperacao = true;
                TotaisHoras = relogio.pausa.Subtract(relogio.horas);
                _horas = TotaisHoras.Hours;
                _minutos = TotaisHoras.Minutes;
                _segundos = TotaisHoras.Seconds;
                _milesegundos = TotaisHoras.Milliseconds;

            }
            if(_milesegundos==0)
                timer = new System.Timers.Timer();

            System.Threading.Tasks.Task.Run(async () =>
            {
                // TODO: do something long-running
                // await System.Threading.Tasks.Task.Delay(50000000);

                // Invoke Complete to signal you're done.
                
                timer.Interval = 1; // 1 milliseconds  
                timer.Elapsed += Timer_Elapsed; 
                timer.Start();

            });
            
            


        }
        void PausaComThreadSleep()
        {
            Thread.Sleep(1000);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
          

            _milesegundos++;
                if (_milesegundos >= 1000)
                {
                    _segundos++;
                    _milesegundos = 0;
                }
                if (_segundos == 59)
                {
                    _minutos++;
                    _segundos = 0;

                }
                if (_minutos == 59)
                {
                    _horas++;
                    _minutos = 0;
                }


            
           
            Device.BeginInvokeOnMainThread(() =>
            {
                
                    lbl_result.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", _horas, _minutos, _segundos, _milesegundos / 10);
            });
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        }
}
using Android.Text.Style;
using App.Agenda.DataBaseHelper;
using Java.Util;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using static CoreFoundation.DispatchSource;

namespace App.Relatorio.Models
{
    public class Relogio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Dia { get; set; }
        public TimeSpan horas { get; set; }
        public TimeSpan pausa { get; set; }
        public int Status { get; set; } // 0 = relogio em operação 1 = relogio em pausa; 2 = relogio encerrado ou seja horas lançadas

    }

    public static class Cronometro
    {
        private static TimeSpan _cronometro;
        static int _horas = 0, _minutos = 0, _segundos = 0, _milesegundos = 0;
        public static TimeSpan Cronometro_
        {

            get { return _cronometro; }
            set { _cronometro = value; }

        }
        private static bool _isIniciou;
        public static bool IsIniciou
        {

            get { return false; }
            set { _isIniciou = value; }

        }

    

        public static string RetornaTempo()
        {
            DataBase db = new DataBase();
            string retornaTempo = string.Empty;
            //   int _horas = 0, _minutos = 0, _segundos = 0, _milesegundos = 0;
            var relogio = db.GetCronometro().LastOrDefault();
            TimeSpan HorasAtuais = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TimeSpan TotaisHoras = new TimeSpan();
            if (relogio != null)
            {
                if (relogio.Status == 0)
                {

                    TotaisHoras = HorasAtuais.Subtract(relogio.horas);
                    _horas = TotaisHoras.Hours;
                    _minutos = TotaisHoras.Minutes;
                    _segundos = TotaisHoras.Seconds;
                    _milesegundos = TotaisHoras.Milliseconds;

                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Interval = 1; // 1 milliseconds  
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();

                    return retornaTempo = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", _horas, _minutos, _segundos, _milesegundos / 10);

                }
                return "00:00:000";
            }
            return "00:00:000";
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
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

        }

    }
}

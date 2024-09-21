using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace App.Relatorio.Models
{
 

    public class AnotacoesDoMEs
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Guid indentificadorFirebase { get; set; }

        public string NomePublicador { get; set; }


        private string _mesano;
        public string MesAno
        {

            get { return _mesano; }
            set { _mesano = value; }

        }


        public string Horas { get; set; }
        public int Publicacoes { get; set; }
        public int Videos { get; set; }
        public int Revisitas { get; set; }
        public int Estudos { get; set; }
        public string Observacao { get; set; }
        public DateTime MesDeServico { get; set; }

        public string _mesExtensao { get; set; }
        public string MesExtenso
        {

            get { return _mesExtensao; }
            set { _mesExtensao = value; }



        }


        public static string ExibirMesPorExtenso(string data)
        {
            DateTime enteredDate = DateTime.Parse(data);

            return enteredDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-br")).ToUpper();
        }


    }
}

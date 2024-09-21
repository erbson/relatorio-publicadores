using App.Agenda.DataBaseHelper;
using App.Relatorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace App.Relatorio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginInicialPage : ContentPage
    {
        DataBase db = new DataBase();
        public LoginInicialPage()
        {
            InitializeComponent();
            db.CriarBancoDeDados();
        }

        private async void btGravar_Clicked(object sender, EventArgs e)
        {
            Publicador publicador = new Publicador();

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                DisplayAlert("Atenção", "Informe o nome completo!", "Ok");
                return;
            }

            if (txtNome.Text.Length <= 7)
            {
                DisplayAlert("Atenção", "Informe o nome completo!", "Ok");
                return;

            }

            if (dropGrup.SelectedItem == null)
            {
                DisplayAlert("Atenção", "Informe o Grupo por favor!", "Ok");
                return;

            }
            publicador.Nome = txtNome.Text;
            publicador.Grupo = dropGrup.SelectedItem.ToString();







            if (db.InseriPublicador(publicador))
            {

                DisplayAlert("Obrigado!", "Agora saia do Aplicativo e entre novamente", "Yes");
            }
            else
            {
                DisplayAlert("ERRO", "algum erro aconteu ao tentar inserir seu cadastro!", "Ok");

            }


            }
        }
     }
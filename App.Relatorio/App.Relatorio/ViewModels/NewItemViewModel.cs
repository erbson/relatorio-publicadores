using App.Agenda.DataBaseHelper;
using App.Relatorio.Models;
using App.Relatorio.Views;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.Relatorio.ViewModels
{

  
    public class NewItemViewModel : BaseViewModel
    {
        private string nome;
        private string _botaoRelatorio;
        private string txtMes;
        private string txtobservacoes;
        DataBase bd = new DataBase();
        private string txtHoras;
        private int  txtrevisitas;
        private int  txtpublicacoes;
        private int txtvideos;
        private int txtestudos;
        private DateTime _messevico;
        readonly string RelatorioResource = "RelatorioAtividade";
        private bool IsGuardarHoras = false;
        private string _Cabecalho;
        private string txtStatusInternet;

        bool _dateToPickerVisibility;
        FirebaseClient ConnectFirebase;

        private int mesAtividade;


        public NewItemViewModel(TimeSpan horas,bool salvar=false)
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CarregarCommand = new Command(Load);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            if (salvar)
            {
                MesDeServico = DateTime.Now;
                BotaoRelatorio = "Salvar Atividade";
                txtHoras = horas.ToString(@"hh\:mm");
            }
            else
            {
                MesDeServico = DateTime.Now.Date.AddMonths(-1);
                BotaoRelatorio = "Transmitir Relatorio";
                Load();
            }
           
            DateToPickerVisibility = true;
            _Cabecalho = "Atenção ao clicar em transmitir o seu relatorio será enviado ao secretario da congregação!";


        IsGuardarHoras = salvar;
        }

        private bool ValidateSave()
        {
            //return !String.IsNullOrWhiteSpace(TxtHoras.ToString());
            return true;
                
        }
        public string Cabecalho
        {
            get => _Cabecalho;
            set => SetProperty(ref _Cabecalho, value);
        }
        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }
        private bool checkboxvisible;
        public bool ChekBoxVisible
        {
            get { if (IsGuardarHoras == false) return true;
                return false;
            }
            set { checkboxvisible = value; }
        }
        public string BotaoRelatorio
        {
            get => _botaoRelatorio;
            set => SetProperty(ref _botaoRelatorio, value);
        }


        public bool DateToPickerVisibility
        {
            set { SetProperty(ref _dateToPickerVisibility, value); }
            get { return _dateToPickerVisibility; }
        }

        DateTime _dateTo;
        public DateTime MesDeServico
        {
            set { SetProperty(ref _dateTo, value); }
            get { return _dateTo; }
        }

        public string TxtHoras
        {
            get => txtHoras;
            set => SetProperty(ref txtHoras, value);
        }
        private bool _checkPioneiro;
        public bool CheckPioneiro
        {
            get => _checkPioneiro;
            set => SetProperty(ref _checkPioneiro, value);
        }

        public int TxtRevisitas
        {
            get => txtrevisitas;
            set => SetProperty(ref txtrevisitas, value);
        }

        //public DateTime MesDeServico
        //{
        //    get => _messevico;
        //    set => SetProperty(ref _messevico, value);
        //}

        public int  TxtPublicacoes
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

        public string TxtStatusInternet
        {
            get => txtStatusInternet;
            set => SetProperty(ref txtStatusInternet, value);
        }

        public Command SaveCommand { get; }
        public Command CarregarCommand { get; }
        public Command CancelCommand { get; }
        public int MesAtividade
        {
            get => mesAtividade; set => SetProperty(ref mesAtividade, value);
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public Task<bool> ConexaoFirebase()
        {
            try
            {
                if (ConnectFirebase == null)
                {
                    ConnectFirebase = new FirebaseClient("");
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }


            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }


        private async void Load()
        {

            await App.Current.MainPage.DisplayAlert("App diz:", " Antes de transmitir o seu relatório, por favor ajuste as horas do mesmo, para um número inteiro, não fracionado somente horas sem os minutos, duvidas entre em contato com o secretario ", "Ok", "Não");
            var relatorioDoMes = bd.GetRelatorios().Where(x => x.MesDeServico.Month == MesDeServico.Month && x.MesDeServico.Year == MesDeServico.Year).FirstOrDefault();



            if (relatorioDoMes != null)
            {

                TxtHoras = relatorioDoMes.Horas;
                TxtEstudos = relatorioDoMes.Estudos;
                TxtVideos = relatorioDoMes.Videos;
                TxtPublicacoes = relatorioDoMes.Publicacoes;
                TxtRevisitas = relatorioDoMes.Revisitas;
            }
        }
        private async void OnSave()
        {
            var message = DependencyService.Get<IMessageService>();





            if (string.IsNullOrWhiteSpace(TxtHoras))
            {
                await App.Current.MainPage.DisplayAlert("App diz:", " Horas não informada, se caso você não relatou entre em contato com o superitendente de Grupo ", "Ok", "Não");
                return;
            }


            if (IsGuardarHoras)
            {

                var relatorioDoMes = bd.GetRelatorios().Where(x => x.MesDeServico.Month == MesDeServico.Month && x.MesDeServico.Year == MesDeServico.Year).FirstOrDefault();
                if (relatorioDoMes != null)
                {
                    string NovasHoras = (TimeSpan.Parse(TxtHoras) + TimeSpan.Parse(relatorioDoMes.Horas)).ToString(@"hh\:mm");
                    Models.Relatorio relatorio = new Models.Relatorio()
                    {
                        NomePublicador = bd.GetPublicador(),
                        Id = relatorioDoMes.Id,
                        Horas = NovasHoras,
                        Estudos = TxtEstudos,
                        Videos = TxtVideos + relatorioDoMes.Videos,
                        Publicacoes = TxtPublicacoes + relatorioDoMes.Publicacoes,
                        Revisitas = TxtRevisitas + relatorioDoMes.Revisitas,
                        Observacao = TxtObservacoes,
                        MesAno = DateTime.Now.ToString(),
                        MesDeServico = MesDeServico,

                    };

                    if (bd.Update(relatorio))
                    {
                        Models.AnotacoesDoMEs anotacoes = new Models.AnotacoesDoMEs()
                        {
                            NomePublicador = bd.GetPublicador(),
                            Horas = TxtHoras,
                            Estudos = TxtEstudos,
                            Videos = TxtVideos,
                            Publicacoes = TxtPublicacoes,
                            Revisitas = TxtRevisitas,
                            Observacao = TxtObservacoes,
                            MesAno = DateTime.Now.ToString(),
                            MesDeServico = MesDeServico,
                        };

                        bd.InserirAnotacoes(anotacoes);
                        var cronometro = bd.GetCronometro().LastOrDefault();
                        if (cronometro != null && Convert.ToDateTime(cronometro.Dia) == DateTime.Today)
                        {


                            bd.DeletarCronometro(cronometro);



                        }


                        await App.Current.MainPage.DisplayAlert("App diz:", "Horas anotada com sucesso!", "Ok");
                    }


                    return;


                }
                else
                {

                    Models.Relatorio relatorio = new Models.Relatorio()
                    {
                        NomePublicador = bd.GetPublicador(),
                        Horas = TxtHoras,
                        Estudos = TxtEstudos,
                        Videos = TxtVideos,
                        Publicacoes = TxtPublicacoes,
                        Revisitas = TxtRevisitas,
                        Observacao = TxtObservacoes,
                        MesAno = DateTime.Now.ToString(),
                        MesDeServico = MesDeServico


                    };

                    if (bd.InserirRelatorio(relatorio))
                    {

                        Models.AnotacoesDoMEs anotacoes = new Models.AnotacoesDoMEs()
                        {
                            NomePublicador = bd.GetPublicador(),
                            Horas = TxtHoras,
                            Estudos = TxtEstudos,
                            Videos = TxtVideos,
                            Publicacoes = TxtPublicacoes,
                            Revisitas = TxtRevisitas,
                            Observacao = TxtObservacoes,
                            MesAno = DateTime.Now.ToString(),
                            MesDeServico = MesDeServico,
                        };

                        bd.InserirAnotacoes(anotacoes);
                        await App.Current.MainPage.DisplayAlert("App diz:", "Horas anotada com sucesso!", "Ok");

                    }

                    return;
                }
            }




            if (await ConexaoFirebase())
            {

                DateTime.TryParseExact(MesDeServico.ToString(), "MMyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out
         DateTime DataConvertida);

                Models.Relatorio relatorio = new Models.Relatorio()
                {
                    NomePublicador = bd.GetPublicador(),
                    Horas = TxtHoras,
                    Estudos = TxtEstudos,
                    Videos = TxtVideos,
                    Publicacoes = TxtPublicacoes,
                    Revisitas = TxtRevisitas,
                    Observacao = TxtObservacoes,
                    MesAno = MesDeServico.ToString("yyyy MMM"),
                    MesDeServico = MesDeServico,
                    indentificadorFirebase = Guid.NewGuid(),
                    PioneiroAuxiliar = CheckPioneiro == true ? "Sim" : "Não"

                };

                // var response =  message.ShowAsync("Tem certeza que deseja enviar agora o seu relatorio ?");
                // message.ShowAsync("Tem certeza que deseja enviar agora o seu relatorio ?");


                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await App.Current.MainPage.DisplayAlert("App diz:", "Tem certeza que deseja enviar agora o seu relatorio ? ", "Ok", "Não");
                    if (result)
                    {
                        if (bd.InserirRelatorio(relatorio))
                        {

                            //await ConnectFirebase.Child(RelatorioResource).PostAsync(JsonConvert.SerializeObject(relatorio));

                            await ConnectFirebase
                           .Child("Relatorio")
                          .PostAsync(new Models.Relatorio()
                          {
                              Id = relatorio.Id,
                              NomePublicador = relatorio.NomePublicador,
                              MesAno = relatorio.MesAno,
                              Horas = relatorio.Horas,
                              Publicacoes = relatorio.Publicacoes,
                              Videos = relatorio.Videos,
                              Revisitas = relatorio.Revisitas,
                              Estudos = relatorio.Estudos,
                              MesDeServico = relatorio.MesDeServico,
                              Observacao = relatorio.Observacao,
                              PioneiroAuxiliar = CheckPioneiro==true?"Sim":"Não"

                          });

                            await App.Current.MainPage.DisplayAlert("App diz:", "Seu relatorio foi enviado com sucesso, agradecemos a prontidão!", "Ok");
                            await Shell.Current.GoToAsync("..");
                        }


                        // System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow(); // Or anything else
                    }
                });

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("App diz:", " Algum erro aconteceu!, por favor verifique sua conexao com a internete ou entre em contato com administrador do aplicativo ", "Ok", "Não");

            }

        }




    }
}

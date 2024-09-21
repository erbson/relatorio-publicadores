using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.Relatorio.ViewModels
{
    public class MessageService : IMessageService
    {
        public  bool ShowAsync(string message)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await App.Current.MainPage.DisplayAlert("App diz:", message, "Ok", "Não");
                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow(); // Or anything else
                }
            });

            return true;

        }
    }
}

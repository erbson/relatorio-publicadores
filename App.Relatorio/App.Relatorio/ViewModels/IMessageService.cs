using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Relatorio.ViewModels
{
   public   interface IMessageService
    {
        bool ShowAsync(string message);
    }
}

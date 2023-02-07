using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.ViewModels
{
    public class MenuViewModel:BaseViewModel
    {
        private string _userid;
        public string userid { get => _userid; set => _userid = value; }
        public MenuViewModel()
        {
           
        }
    }
}

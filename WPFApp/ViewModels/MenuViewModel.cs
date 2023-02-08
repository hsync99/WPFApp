using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFApp.ViewModels
{
    public class MenuViewModel:BaseViewModel
    {
        private string _userid;
        public string userid { get => _userid; set => _userid = value; }
        private Button _btn;
        public Button Btn { get=> _btn; set => _btn = value; }  
        private List<Button> _buttons;
        public List<Button> Buttons { get=>_buttons; set => _buttons = value; } 
        public MenuViewModel()
        {
            


            Buttons = new List<Button>();
            for (int i = 0; i < 20; i++)
            {
                Btn = new Button();
                Btn.Height = 50;
                Btn.Width = 50;
                Buttons.Add(Btn);
            }
            

        }
    }
}

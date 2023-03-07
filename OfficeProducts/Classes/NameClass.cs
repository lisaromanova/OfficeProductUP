using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OfficeProducts.Classes
{
    public class NameClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string name = "";
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        int id = 0;
        public int GetUserID
        {
            get => id;
            set
            {
                id = value;
            }
        }

        Visibility btnVisible = Visibility.Hidden;
        public Visibility ButtonVisible
        {
            get => btnVisible;
            set
            {
                btnVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ButtonVisible"));
            }
        }
    }
}

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

        User user;
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

        public User GetUser
        {
            get => user;
            set
            {
                user = value;
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

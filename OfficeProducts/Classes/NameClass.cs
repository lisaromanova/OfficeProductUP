using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeProducts.Classes
{
    public class NameClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string name = "A";
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }
}

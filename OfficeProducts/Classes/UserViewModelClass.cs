using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OfficeProducts.Classes
{
    public class UserViewModelClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        User user;
        public string Name
        {
            get
            {
                if (user != null)
                {
                    return user.UserFio;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public User GetUser
        {
            get => user;
            set
            {
                user = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OfficeProducts
{
    public partial class User
    {
        public string UserFio
        {
            get => UserSurname + " " + UserName[0] + ". " + UserPatronymic[0] + ".";
        }
    }
}

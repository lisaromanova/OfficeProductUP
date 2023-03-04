using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OfficeProducts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Classes.DataBaseClass.connect = new DataBaseEntities();
            Classes.FrameClass.frmMain = frmMain;
            Classes.NameClass name = new Classes.NameClass();
            DataContext = name;
            Classes.FrameClass.frmMain.Navigate(new Pages.AuthorizationPage(name));
        }
    }
}

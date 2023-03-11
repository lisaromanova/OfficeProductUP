using OfficeProducts.Classes;
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

namespace OfficeProducts.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkWithOrderPage.xaml
    /// </summary>
    public partial class WorkWithOrderPage : Page
    {
        NameClass name;
        public WorkWithOrderPage(NameClass name)
        {
            InitializeComponent();
            this.name = name;
            lstOrder.ItemsSource = Classes.DataBaseClass.connect.Order.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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
        UserViewModelClass name;
        public WorkWithOrderPage(UserViewModelClass name)
        {
            InitializeComponent();
            this.name = name;
            lstOrder.ItemsSource = Classes.DataBaseClass.connect.Order.ToList();
        }

        /// <summary>
        /// Фильтрация и сортировка данных
        /// </summary>
        void Filter()
        {
            List<Order> list = Classes.DataBaseClass.connect.Order.ToList();
            if (cbSort.SelectedIndex != -1 && cbSort.SelectedIndex != 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.SumOrder).ToList();
                        break;
                    case 2:
                        list = list.OrderByDescending(x => x.SumOrder).ToList();
                        break;
                }
            }
            if (cbDiscount.SelectedIndex != -1 && cbDiscount.SelectedIndex != 0)
            {
                switch (cbDiscount.SelectedIndex)
                {
                    case 1:
                        list = list.Where(x => x.SumDiscount >= 0 && x.SumDiscount < 11).ToList();
                        break;
                    case 2:
                        list = list.Where(x => x.SumDiscount >= 11 && x.SumDiscount < 15).ToList();
                        break;
                    case 3:
                        list = list.Where(x => x.SumDiscount >= 15).ToList();
                        break;
                }
            }
            if (list.Count > 0)
            {
                tbEmpty.Visibility = Visibility.Collapsed;
                lstOrder.Visibility = Visibility.Visible;
                lstOrder.ItemsSource = list;
            }
            else
            {
                lstOrder.Visibility = Visibility.Collapsed;
                tbEmpty.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}

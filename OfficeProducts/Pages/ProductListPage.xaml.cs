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
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        int countList;
        public ProductListPage()
        {
            InitializeComponent();
            List<Product> products = Classes.DataBaseClass.connect.Product.ToList();
            lstProduct.ItemsSource = products;
            countList = products.Count;
        }

        void Filter()
        {
            List<Product> list = Classes.DataBaseClass.connect.Product.ToList();
            if(cbSort.SelectedIndex != -1)
            {
                switch(cbSort.SelectedIndex)
                {
                    case 0:
                        list = list.OrderBy(x => x.CostSort).ToList();
                        break;
                    case 1:
                        list = list.OrderByDescending(x => x.CostSort).ToList();
                        break;
                }
            }
            if(cbDiscount.SelectedIndex != -1 && cbDiscount.SelectedIndex!=0)
            {
                switch(cbDiscount.SelectedIndex)
                {
                    case 1:
                        list = list.Where(x => x.ProductDiscountMax >= 0 && x.ProductDiscountMax < 10).ToList();
                        break;
                    case 2:
                        list = list.Where(x => x.ProductDiscountMax >= 10 && x.ProductDiscountMax < 15).ToList();
                        break;
                    case 3:
                        list = list.Where(x => x.ProductDiscountMax >= 15).ToList();
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                list = list.Where(x=> x.ProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if(list.Count > 0)
            {
                tbEmpty.Visibility = Visibility.Collapsed;
                lstProduct.Visibility = Visibility.Visible;
                lstProduct.ItemsSource = list;
            }
            else
            {
                lstProduct.Visibility = Visibility.Collapsed;
                tbEmpty.Visibility = Visibility.Visible;
            }
            tbCount.Text = list.Count.ToString() + " из " + countList.ToString();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
    }
}

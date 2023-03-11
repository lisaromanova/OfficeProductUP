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
        Order order;
        public ProductListPage(Classes.NameClass name)
        {
            InitializeComponent();
            List<Product> products = Classes.DataBaseClass.connect.Product.ToList();
            lstProduct.ItemsSource = products;
            countList = products.Count;
            tbCount.Text = products.Count.ToString() + " из " + countList.ToString();
            order = new Order();
            order.OrderStatus = 1;
            order.OrderDate = DateTime.Now;
            if(name.GetUserID != 0)
            {
                order.UserID = name.GetUserID;
            }
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
                        list = list.Where(x => x.ProductDiscountAmount >= 0 && x.ProductDiscountAmount < 10).ToList();
                        break;
                    case 2:
                        list = list.Where(x => x.ProductDiscountAmount >= 10 && x.ProductDiscountAmount < 15).ToList();
                        break;
                    case 3:
                        list = list.Where(x => x.ProductDiscountAmount >= 15).ToList();
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

        List<OrderProduct> listOrder = new List<OrderProduct>();

        private void addToOrder_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            string id = item.Uid.ToString();
            Product product = Classes.DataBaseClass.connect.Product.FirstOrDefault(x => x.ProductArticleNumber == id);
            btnViewOrder.Visibility = Visibility.Visible;
            int k = -1;
            for (int i =0; i<listOrder.Count; i++)
            {
                if (listOrder[i].ProductArticleNumber == id)
                {
                    k = i;
                }
            }
            if (k == -1)
            {
                OrderProduct orderProduct = new OrderProduct()
                {
                    OrderID = order.OrderID,
                    ProductArticleNumber = id,
                    Quantity = 1,
                    Product = product
                };
                listOrder.Add(orderProduct);
            }
            else
            {
                listOrder[k].Quantity++;
            }
        }

        private void btnViewOrder_Click(object sender, RoutedEventArgs e)
        {
            ViewOrderWindow view = new ViewOrderWindow(order, listOrder);
            view.ShowDialog();
        }
    }
}

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
        Classes.UserViewModelClass name;

        public ProductListPage(Classes.UserViewModelClass name)
        {
            InitializeComponent();
            List<Product> products = Classes.DataBaseClass.connect.Product.ToList();
            lstProduct.ItemsSource = products;
            countList = products.Count;
            tbCount.Text = products.Count.ToString() + " из " + countList.ToString();
            order = new Order();
            order.OrderStatus = 1;
            order.OrderDate = DateTime.Now;
            if(name.GetUser != null)
            {
                order.UserID = name.GetUser.UserID;
                order.User = name.GetUser;
            }
            this.name = name;
            if (name.GetUser != null)
            {
                switch (name.GetUser.Logined.UserRoleID)
                {
                    case 3:
                        btnOrders.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        btnOrders.Visibility = Visibility.Visible;
                        btnAddProduct.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        /// <summary>
        /// Фильтрация и сортировка данных
        /// </summary>
        void Filter()
        {
            List<Product> list = Classes.DataBaseClass.connect.Product.ToList();
            if(cbSort.SelectedIndex != -1 && cbSort.SelectedIndex!=0)
            {
                switch(cbSort.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.CostSort).ToList();
                        break;
                    case 2:
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
            Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.frmMain.Navigate(new WorkWithOrderPage(name));
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.Uid.ToString();
            List<OrderProduct> list = Classes.DataBaseClass.connect.OrderProduct.Where(x => x.ProductArticleNumber == id).ToList();
            if(list.Count == 0)
            {
                Product product = Classes.DataBaseClass.connect.Product.FirstOrDefault(x => x.ProductArticleNumber == id);
                MessageBoxResult result =  MessageBox.Show($"Вы точно хотите удалить {product.ProductName}?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Classes.DataBaseClass.connect.Product.Remove(product);
                        Classes.DataBaseClass.connect.SaveChanges();
                        MessageBox.Show("Продукт удален!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Нельзя удалить продукт, так как он используется в заказах!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

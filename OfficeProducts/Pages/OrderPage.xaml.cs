using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<OrderProduct> orderProduct;
        Order order;
        public OrderPage(Order order, List<OrderProduct> orderProduct)
        {
            InitializeComponent();
            this.order = order;
            this.orderProduct = orderProduct;
            lstProduct.ItemsSource = orderProduct;
            cbPickPoint.ItemsSource = Classes.DataBaseClass.connect.PickPoint.ToList();
            cbPickPoint.DisplayMemberPath = "PickPointName";
            cbPickPoint.SelectedValuePath = "PickPointID";
            CalculationSum();
            if(order.User != null)
            {
                tbUser.Text = order.User.UserSurname + " " + order.User.UserName[0] + ". " + order.User.UserPatronymic[0]+".";
            }
        }

        double sum = 0;
        double sumDiscount;
        /// <summary>
        /// Подсчет итоговой суммы и итоговой скидки
        /// </summary>
        void CalculationSum()
        {
            sum = 0;
            double sumWithoutDuscount = 0;
            foreach (OrderProduct product in orderProduct)
            {
                sum += product.Product.CostSort * product.Quantity;
                sumWithoutDuscount += Convert.ToDouble(product.Product.Cost) * product.Quantity;
            }
            double disc;
            if (sumWithoutDuscount != 0)
            {
                disc = 10 - (sum / sumWithoutDuscount);
            }
            else
            {
                disc = 0;
            }
            sum = Math.Round(sum, 2);
            sumDiscount = Math.Round(disc, 2);
            tbSum.Text = sum.ToString();
            tbSumDiscount.Text = sumDiscount.ToString() + "%";
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string article = tb.Uid.ToString();
            OrderProduct product = orderProduct.FirstOrDefault(x => x.ProductArticleNumber == article);
            if (product.Quantity == 0)
            {
                DeleteProductFromOrder(product);
            }
            if (Regex.IsMatch(tb.Text, "^\\d+$"))
            {
                btnCheckout.Visibility = Visibility.Visible;
            }
            else
            {
                btnCheckout.Visibility = Visibility.Collapsed;
            }
            CalculationSum();
        }

        /// <summary>
        /// Удаление продукта из заказа
        /// </summary>
        /// <param name="product">Объект продукта</param>
        void DeleteProductFromOrder(OrderProduct product)
        {
            orderProduct.Remove(product);
            lstProduct.ItemsSource = null;
            lstProduct.ItemsSource = orderProduct;
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (cbPickPoint.SelectedIndex != -1)
            {
                if (orderProduct.Count != 0)
                {
                    order.OrderPickupPointID = (int)cbPickPoint.SelectedValue;
                    int k = 0;
                    foreach (OrderProduct product in orderProduct)
                    {
                        if (product.Product.ProductQuantityInStock == 0 || product.Product.ProductQuantityInStock <= 3)
                        {
                            k++;
                        }
                    }
                    if (k == 0)
                    {
                        order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                    }
                    else
                    {
                        order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                    }
                    List<Order> list = Classes.DataBaseClass.connect.Order.ToList();
                    order.NumberReceiving = list[list.Count - 1].NumberReceiving + 1;
                    Classes.DataBaseClass.connect.Order.Add(order);

                    foreach (OrderProduct product in orderProduct)
                    {
                        Classes.DataBaseClass.connect.OrderProduct.Add(product);
                    }
                    Classes.DataBaseClass.connect.SaveChanges();
                    MessageBox.Show("Заказ успешно создан", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Classes.FrameClass.frmOrder.Navigate(new TicketPage(order, orderProduct, sum, sumDiscount));

                }
                else
                {
                    MessageBox.Show("Выберите продукт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите пункт выдачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string article = btn.Uid.ToString();
            OrderProduct product = orderProduct.FirstOrDefault(x => x.ProductArticleNumber == article);
            DeleteProductFromOrder(product);
            CalculationSum();
        }
    }
}

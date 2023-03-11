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
using System.Windows.Shapes;

namespace OfficeProducts
{
    /// <summary>
    /// Логика взаимодействия для ViewOrderWindow.xaml
    /// </summary>
    public partial class ViewOrderWindow : Window
    {
        List<OrderProduct> orderProduct;
        Order order;
        public ViewOrderWindow(Order order, List<OrderProduct> orderProduct)
        {
            InitializeComponent();
            //Classes.DataBaseClass.connect.Order.Add(order);
            this.order = order;
            this.orderProduct = orderProduct;
            lstProduct.ItemsSource = orderProduct;
            cbPickPoint.ItemsSource = Classes.DataBaseClass.connect.PickPoint.ToList();
            cbPickPoint.DisplayMemberPath = "PickPointName";
            cbPickPoint.SelectedValuePath = "PickPointID";
            CalculationSum();
        }

        /// <summary>
        /// Подсчет итоговой суммы и итоговой скидки
        /// </summary>
        void CalculationSum()
        {
            double sum = 0;
            double sumWithoutDuscount = 0;
            foreach (OrderProduct product in orderProduct)
            {
                sum += product.Product.CostSort * product.Quantity;
                sumWithoutDuscount += Convert.ToDouble(product.Product.Cost) * product.Quantity;
            }
            double disc;
            if (sumWithoutDuscount != 0)
            {
                disc = 100 - (100 * sum / sumWithoutDuscount);
            }
            else
            {
                disc = 0;
            }
            tbSum.Text = Math.Round(sum, 2).ToString();
            tbSumDiscount.Text = Math.Round(disc, 2).ToString() + "%";
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeleteProductFromOrder();
            CalculationSum();
        }

        /// <summary>
        /// Удаление продукта из заказа
        /// </summary>
        void DeleteProductFromOrder()
        {
            List<OrderProduct> list = new List<OrderProduct>();
            foreach (OrderProduct product in orderProduct)
            {
                if (product.Quantity == 0)
                {
                    list.Add(product);
                }
            }
            foreach (OrderProduct product in list)
            {
                orderProduct.Remove(product);
            }
            lstProduct.ItemsSource = null;
            lstProduct.ItemsSource = orderProduct;
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if(cbPickPoint.SelectedIndex != -1)
            {
                order.OrderPickupPointID = (int)cbPickPoint.SelectedValue;
                int k = 0;
                
                foreach (OrderProduct product in orderProduct)
                {
                    if (product.Product.ProductQuantityInStock == null || product.Product.ProductQuantityInStock <= 3)
                    {
                        k++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пункт выдачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

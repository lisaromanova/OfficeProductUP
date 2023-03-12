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
    /// Логика взаимодействия для AlterStatusWindow.xaml
    /// </summary>
    public partial class AlterStatusWindow : Window
    {
        Order order;
        public AlterStatusWindow(Order order)
        {
            InitializeComponent();
            cbStatus.ItemsSource = Classes.DataBaseClass.connect.OrderStatus.ToList();
            cbStatus.SelectedValuePath = "OrderStatusID";
            cbStatus.DisplayMemberPath = "OrderStatusName";
            cbStatus.SelectedValue = order.OrderStatus;
            this.order = order;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cbStatus.SelectedIndex != -1)
            {
                order.OrderStatus = (int)cbStatus.SelectedValue;
                Classes.DataBaseClass.connect.SaveChanges();
                MessageBox.Show("Изменения сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Выберите значение из выпадающего списка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

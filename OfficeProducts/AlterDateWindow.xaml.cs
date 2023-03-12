using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AlterDateWindow.xaml
    /// </summary>
    public partial class AlterDateWindow : Window
    {
        Order order;
        public AlterDateWindow(Order order)
        {
            InitializeComponent();
            this.order = order;
            dtDate.SelectedDate = order.OrderDeliveryDate;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(dtDate.SelectedDate != null)
            {
                order.OrderDeliveryDate = (DateTime)dtDate.SelectedDate;
                Classes.DataBaseClass.connect.SaveChanges();
                MessageBox.Show("Изменения сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Выберите дату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

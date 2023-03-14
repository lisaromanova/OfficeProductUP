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
using System.Windows.Threading;

namespace OfficeProducts.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        DispatcherTimer timer;
        Classes.UserViewModelClass name;
        public AuthorizationPage(Classes.UserViewModelClass name)
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += Timer_Tick;
            this.name = name;
        }

        string str = "";

        /// <summary>
        /// Метод для генерации capthca
        /// </summary>
        void Captcha()
        {
            Random random = new Random();
            int n = random.Next(4, 8);
            double left = 10;
            double count = (canvasCaptcha.Width - 20) / n;
            for (int i = 0; i < n; i++)
            {
                int x = random.Next(3);
                string name = "";
                int x1 = random.Next(2);
                switch (x)
                {
                    case 0:
                        name = ((char)random.Next('A', 'Z')).ToString();
                        break;
                    case 1:
                        name = random.Next(9).ToString();
                        break;
                    case 2:
                        name = ((char)random.Next('a', 'z')).ToString();
                        break;
                }
                str += name;
                TextBlock tb = new TextBlock()
                {
                    Text = name,
                    FontSize = 28,
                    FontFamily = new FontFamily("Comic Sans MS"),
                };
                Canvas.SetLeft(tb, left);
                Canvas.SetTop(tb, random.Next(Convert.ToInt32(canvasCaptcha.Height - 35)));
                left += count;
                switch (x1)
                {
                    case 0:
                        tb.FontWeight = FontWeights.Bold;
                        break;
                    case 1:
                        tb.FontStyle = FontStyles.Italic;
                        break;
                    case 2:
                        tb.FontWeight = FontWeights.Bold;
                        tb.FontStyle = FontStyles.Italic;
                        break;
                }
                tb.TextDecorations = TextDecorations.Strikethrough;
                canvasCaptcha.Children.Add(tb);
            }
            int n1 = random.Next(6, 10);
            for (int i = 0; i < n1; i++)
            {
                Line line = new Line()
                {
                    X1 = random.Next(Convert.ToInt32(canvasCaptcha.Width)),
                    X2 = random.Next(Convert.ToInt32(canvasCaptcha.Width)),
                    Y1 = random.Next(Convert.ToInt32(canvasCaptcha.Height)),
                    Y2 = random.Next(Convert.ToInt32(canvasCaptcha.Height)),
                    Stroke = new SolidColorBrush(Color.FromRgb(Convert.ToByte(random.Next(256)), Convert.ToByte(random.Next(256)), Convert.ToByte(random.Next(256))))
                };
                canvasCaptcha.Children.Add(line);
            }
        }

        bool check = false;

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                if (!string.IsNullOrWhiteSpace(pswPassword.Password))
                {
                    Logined user = Classes.DataBaseClass.connect.Logined.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == pswPassword.Password);
                    if (!check)
                    {
                        if (user != null)
                        {
                            name.ButtonVisible = Visibility.Visible;
                            name.GetUser = user.User;
                            MessageBox.Show("Успешная авторизация!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
                        }
                        else
                        {
                            MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            Captcha();
                            tbLogin.Text = string.Empty;
                            pswPassword.Password = string.Empty;
                            stackCaptcha.Visibility = Visibility.Visible;
                            check = true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(tbCaptcha.Text))
                        {
                            if (user != null && tbCaptcha.Text == str)
                            {
                                name.GetUser = user.User;
                                name.ButtonVisible = Visibility.Visible;
                                MessageBox.Show("Успешная авторизация!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                                Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
                            }
                            else
                            {
                                MessageBox.Show("Неверные данные! Войти заново можно будет через 10 секунд", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                tbLogin.Text = string.Empty;
                                pswPassword.Password = string.Empty;
                                tbCaptcha.Text = string.Empty;
                                tbLogin.IsEnabled = false;
                                pswPassword.IsEnabled = false;
                                tbCaptcha.IsEnabled = false;
                                timer.Start();
                                btnEnter.Visibility = Visibility.Hidden;
                                btnEnterGuest.Visibility = Visibility.Hidden;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите Captcha!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Заполните поле Пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните поле Логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            canvasCaptcha.Children.Clear();
            Captcha();
            tbLogin.IsEnabled = true;
            pswPassword.IsEnabled = true;
            tbCaptcha.IsEnabled = true;
            btnEnter.Visibility = Visibility.Visible;
            btnEnterGuest.Visibility = Visibility.Visible;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            name.ButtonVisible = Visibility.Visible;
            Classes.FrameClass.frmMain.Navigate(new ProductListPage(name));
        }
    }
}

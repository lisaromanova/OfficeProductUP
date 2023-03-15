using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.IO;
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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using OfficeProducts.Classes;

namespace OfficeProducts.Pages
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        Order order;
        double sum, sumDiscount;
        List<OrderProduct> products;
        public TicketPage(Order order, List<OrderProduct> products, double sum, double sumDiscount)
        {
            InitializeComponent();
            this.order = order;
            this.sum = sum;
            this.products = products;
            this.sumDiscount = sumDiscount;
            UserViewModelClass.OrderFinal = true;
            rDateOrder.Text = order.OrderDate.ToString("dd MM yyyy");
            rDate.Text = order.OrderDeliveryDate.ToString("dd MM yyyy");
            rNumber.Text = order.OrderID.ToString();
            rSum.Text = sum.ToString();
            rDiscount.Text = sumDiscount.ToString() + "%";
            rKod.Text = order.NumberReceiving.ToString();
            rOrder.Text = order.PickPoint.PickPointName;
            string orderProduct = "";
            foreach(OrderProduct product in products)
            {
                orderProduct += product.Product.ProductName + " " +product.Product.ManufacturerProduct.ManufacturerName+", " + product.Quantity + product.Product.UnitOfMeasurement.UnitOfMeasurement1 + "\n";
            }
            tbOrder.Text = orderProduct;
            if (order.User != null)
            {
                user.Visibility = Visibility.Visible;
                tbUser.Text = order.User.UserFio;
            }
        }

        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Comic Sans MS", 20, XFontStyle.Bold);
            XFont fontBase = new XFont("Comic Sans MS", 16, XFontStyle.Bold);
            XFont fontString = new XFont("Comic Sans MS", 16, XFontStyle.Regular);
            gfx.DrawString("Талон", font, XBrushes.Black,
                new XPoint(page.Width / 2, 70));
            double height = 110;
            double width = 184;
            gfx.DrawString("Номер заказа: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(order.OrderID.ToString(), fontString, XBrushes.Black,
                 new XPoint(width, height));
            height += 30;
            gfx.DrawString("Дата заказа: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(order.OrderDate.ToString("dd MM yyyy"), fontString, XBrushes.Black,
                 new XPoint(width, height));
            height += 30;
            gfx.DrawString("Дата доставки: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(order.OrderDeliveryDate.ToString("dd MM yyyy"), fontString, XBrushes.Black,
                new XPoint(width, height));
            height += 30;
            gfx.DrawString("Сумма заказа: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(sum.ToString(), fontString, XBrushes.Black,
                new XPoint(width, height));
            height += 30;
            gfx.DrawString("Сумма скидки: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(sumDiscount.ToString() + "%", fontString, XBrushes.Black,
                new XPoint(width, height));
            height += 30;
            gfx.DrawString("Пункт выдачи: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(order.PickPoint.PickPointName, fontString, XBrushes.Black,
                new XPoint(width, height));
            height += 15;
            gfx.DrawLine(new XPen(XColor.FromName("Black")), 50, height, page.Width - 50, height);
            height += 30;
            gfx.DrawString("Код получения: ", fontBase, XBrushes.Black,
    new XPoint(50, height));
            gfx.DrawString(order.NumberReceiving.ToString(), fontBase, XBrushes.Black,
                new XPoint(width, height));
            if (order.UserID != null)
            {
                height += 30;
                gfx.DrawString("Заказчик: ", fontBase, XBrushes.Black,
        new XPoint(50, height));
                gfx.DrawString(order.User.UserFio.ToString(), fontBase, XBrushes.Black,
                    new XPoint(width, height));
            }
            height += 15;
            gfx.DrawLine(new XPen(XColor.FromName("Black")), 50, height, page.Width - 50, height);
            height += 30;
            gfx.DrawString("Состав заказа: ", fontBase, XBrushes.Black,
        new XPoint(50, height));
            foreach(OrderProduct product in products)
            {
                height += 30;
                gfx.DrawString($"{product.Product.ProductName} {product.Product.ManufacturerProduct.ManufacturerName}, {product.Quantity} {product.Product.UnitOfMeasurement.UnitOfMeasurement1}", fontString, XBrushes.Black,
        new XPoint(50, height));
            }
            string filename = $"\\Order_{order.OrderID}.pdf";
            document.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + filename);
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + filename);
        }
    }
}

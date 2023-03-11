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

namespace OfficeProducts.Pages
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        public TicketPage(Order order, List<OrderProduct> products, double sum, double sumDiscount)
        {
            InitializeComponent();
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
                orderProduct += product.Product.ProductName + ", " + product.Quantity + product.Product.UnitOfMeasurement.UnitOfMeasurement1 + "\n";
            }
            tbOrder.Text = orderProduct;
            if (order.User != null)
            {
                user.Visibility = Visibility.Visible;
                tbUser.Text = order.User.UserSurname + " " + order.User.UserName[0] + ". " + order.User.UserPatronymic[0] + ".";
            }
        }

        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            gfx.DrawString("My Graph", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.TopCenter);
            const string filename = "MyGraph.pdf";
            document.Save(gridTicket + filename);
            Process.Start(filename);
        }
    }
}

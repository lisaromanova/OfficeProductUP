using OfficeProducts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OfficeProducts
{
    public partial class Order
    {

        public double SumOrder
        {
            get
            {
                List<OrderProduct> list = Classes.DataBaseClass.connect.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double sum = 0;
                foreach(OrderProduct products in list)
                {
                    sum += products.Quantity * products.Product.CostSort;
                }
                return Math.Round(sum,2);
            }
        }

        public string Products
        {
            get
            {
                List<OrderProduct> list = Classes.DataBaseClass.connect.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                string s = "";
                foreach (OrderProduct product in list)
                {
                    s += product.Product.ProductName + ", " + product.Quantity + product.Product.UnitOfMeasurement.UnitOfMeasurement1 + "\n";
                }
                return s;
            }
        }

        public string UserName
        {
            get
            {
                if (User != null)
                {
                    return User.UserFio;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public Visibility VisibilityUser
        {
            get
            {
                if (User != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }



        public double SumDiscount
        {
            get
            {
                List<OrderProduct> list = Classes.DataBaseClass.connect.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double sumDiscount = 0;
                foreach (OrderProduct products in list)
                {
                    sumDiscount += products.Quantity * Convert.ToDouble(products.Product.Cost);
                }
                double disc;
                if (sumDiscount != 0)
                {
                    disc = 100 - (100 * SumOrder / sumDiscount);
                }
                else
                {
                    disc = 0;
                }
                return Math.Round(disc, 2);
            }
        }

        public SolidColorBrush OrderColor
        {
            get
            {
                int k = 0;
                int zero = 0;
                List<OrderProduct> orderProduct = Classes.DataBaseClass.connect.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct product in orderProduct)
                {
                    if (product.Product.ProductQuantityInStock == 0 || product.Product.ProductQuantityInStock <= 3)
                    {
                        k++;
                    }
                    if (product.Product.ProductQuantityInStock == 0)
                    {
                        zero++;
                    }
                }
                if (k == 0)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#20b2aa");
                }
                else
                {
                    if(zero != 0)
                    {
                        return (SolidColorBrush)new BrushConverter().ConvertFrom("#20b2aa");
                    }
                    else
                    {
                        return Brushes.White;
                    }
                }
            }
        }
    }
}

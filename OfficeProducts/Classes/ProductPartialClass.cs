using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OfficeProducts
{
    public partial class Product
    {
        public BitmapImage Photo
        {
            get
            {
                if (ProductPhoto != null)
                {
                    return new BitmapImage(new Uri(Environment.CurrentDirectory + ProductPhoto, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    return new BitmapImage(new Uri(@"\Resources\picture.png", UriKind.RelativeOrAbsolute));
                }
            }
        }

        public string NewCost
        {
            get
            {
                if(ProductDiscountAmount!= null)
                {
                    double x = Convert.ToDouble(ProductCost) * Convert.ToDouble(1 - (ProductDiscountAmount / 100));
                    return Math.Round(x, 2).ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public string Cost
        {
            get => ProductCost.ToString().Substring(0, ProductCost.ToString().Length -2);
        }

        public TextDecorationCollection OldCost
        {
            get
            {
                if (ProductDiscountAmount != null)
                {
                    return TextDecorations.Strikethrough;
                }
                else
                {
                    return null;
                }
            }
        }

        public SolidColorBrush ColorProduct
        {
            get
            {
                if (ProductDiscountMax > 15)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#7fff00");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

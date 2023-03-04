using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

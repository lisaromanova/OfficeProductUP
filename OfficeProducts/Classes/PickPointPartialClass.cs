using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeProducts
{
    public partial class PickPoint
    {
        public string PickPointName
        {
            get
            {
                return "г. " + StreetPoint.City.CityName + ", ул." + StreetPoint.StreetName + ", " + PickPointNumber;
            }
        }
    }
}

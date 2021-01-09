using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyMVCClient.Models.Statistics
{
    public class FlightTotalSalePrice
    {
        public int FlightNo { get; set; }

        public double TotalSalePrice { get; set; }

        public int PassengersCount { get; set; }
    }
}

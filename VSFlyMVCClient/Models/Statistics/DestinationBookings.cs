using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyMVCClient.Models.Statistics
{
    public class DestinationBookings
    {
        public string Destination { get; set; }

        public List<BookingStat> BookingSet { get; set; }

        public DestinationBookings()
        {
            BookingSet = new List<BookingStat>();
        }
    }
}

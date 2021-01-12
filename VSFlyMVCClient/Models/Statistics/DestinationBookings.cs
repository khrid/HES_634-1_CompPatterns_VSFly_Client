using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VSFlyMVCClient.Controllers;

namespace VSFlyMVCClient.Models.Statistics
{
    public class DestinationBookings
    {
        public string Destination { get; set; }

        public List<BookingStat> BookingSet { get; set; }

        public async Task<List<SelectListItem>> DestinationsListAsync()
        {
            StatisticsController statisticsController = new StatisticsController();
            return await statisticsController.DestinationsListAsync();
        }

        public DestinationBookings()
        {
            BookingSet = new List<BookingStat>();
        }
    }
}

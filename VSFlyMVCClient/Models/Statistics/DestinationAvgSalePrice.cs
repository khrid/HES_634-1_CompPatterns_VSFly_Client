using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSFlyMVCClient.Controllers;

namespace VSFlyMVCClient.Models.Statistics
{
    public class DestinationAvgSalePrice
    {
        public string Destination { get; set; }

        public double AvgSalePrice { get; set; }

        public async Task<List<SelectListItem>> DestinationsListAsync()
        {
            StatisticsController statisticsController = new StatisticsController();
            return await statisticsController.DestinationsListAsync();
        }
    }
}

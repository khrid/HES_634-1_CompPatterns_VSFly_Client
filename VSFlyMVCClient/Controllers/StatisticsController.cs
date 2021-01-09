using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VSFlyMVCClient.Models.Statistics;

namespace VSFlyMVCClient.Controllers
{
    public class StatisticsController : Controller
    {
        private static HttpClient _httpClient;
        // GET: StatisticsController

        static StatisticsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44350/");
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: FlightsController/Details/5
        public async Task<ActionResult> FlightTotalSalePriceAsync(int flightNo)
        {
            if (flightNo != 0)
            {
                try { 
                HttpResponseMessage response = await _httpClient.GetAsync("api/Statistics/FlightTotalSalePrice/" + flightNo);
                response.EnsureSuccessStatusCode();
                string message = await response.Content.ReadAsStringAsync();

                FlightTotalSalePrice flightTotalSalePrice = JsonConvert.DeserializeObject<FlightTotalSalePrice>(message);

                return View("FlightTotalSalePrice", flightTotalSalePrice);
                }
                catch
                {
                    FlightTotalSalePrice flightTotalSalePrice = new FlightTotalSalePrice();
                    flightTotalSalePrice.FlightNo = -1;
                    return View("FlightTotalSalePrice", flightTotalSalePrice);
                }
            }
            else
            {
                FlightTotalSalePrice flightTotalSalePrice = new FlightTotalSalePrice();
                flightTotalSalePrice.FlightNo = 0;
                return View("FlightTotalSalePrice", flightTotalSalePrice);
            }
        }

        public async Task<ActionResult> DestinationAvgSalePriceAsync(string destination)
        {
            if (!String.IsNullOrEmpty(destination))
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync("/api/Statistics/DestinationAvgSalePrice/" + destination);
                    response.EnsureSuccessStatusCode();
                    string message = await response.Content.ReadAsStringAsync();

                    DestinationAvgSalePrice destinationAvgSalePrice = JsonConvert.DeserializeObject<DestinationAvgSalePrice>(message);

                    return View("DestinationAvgSalePrice", destinationAvgSalePrice);
                }
                catch
                {
                    DestinationAvgSalePrice destinationAvgSalePrice = new DestinationAvgSalePrice();
                    destinationAvgSalePrice.Destination = "notFound";
                    return View("DestinationAvgSalePrice", destinationAvgSalePrice);
                }
            }
            else
            {
                DestinationAvgSalePrice destinationAvgSalePrice = new DestinationAvgSalePrice();
                destinationAvgSalePrice.Destination = String.Empty;
                return View("DestinationAvgSalePrice", destinationAvgSalePrice);
            }
        }

        public async Task<ActionResult> DestinationBookingsAsync(string destination)
        {
            if (!String.IsNullOrEmpty(destination))
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync("/api/Statistics/DestinationBookings/" + destination);
                    response.EnsureSuccessStatusCode();
                    string message = await response.Content.ReadAsStringAsync();

                    DestinationBookings destinationBookings = JsonConvert.DeserializeObject<DestinationBookings>(message);

                    return View("DestinationBookings", destinationBookings);
                }
                catch
                {
                    DestinationBookings destinationBookings = new DestinationBookings();
                    destinationBookings.Destination = "notFound";
                    return View("DestinationBookings", destinationBookings);
                }
            }
            else
            {
                DestinationBookings destinationBookings = new DestinationBookings();
                destinationBookings.Destination = String.Empty;
                return View("DestinationBookings", destinationBookings);
            }
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VSFlyMVCClient.Models;

namespace VSFlyMVCClient.Controllers
{
    public class BookingsController : Controller
    {
        private static HttpClient _httpClient;
        
        static BookingsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44350/");
        }
        // GET: BookingsController
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: BookingsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookingsController/Create
        public ActionResult Create(int id)
        {
            // ugly way for now but we will create a passenger on the fly every time we create a booking
            // TODO : ask if it necessary to implement some kind of register form
            return View(id);
        }

        // POST: BookingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // -----------------
        public async Task<ActionResult> ConfirmAsync(int id)
        {
            TempData["flightNo"] = id;
            HttpResponseMessage response = await _httpClient.GetAsync("api/Flights/" + id);
            response.EnsureSuccessStatusCode();
            string message = await response.Content.ReadAsStringAsync();
            Trace.WriteLine(message);

                
            HttpResponseMessage responseSalePrice = await _httpClient.GetAsync("api/Flights/GetSalePriceForFlight/" + id);
            responseSalePrice.EnsureSuccessStatusCode();
            string messageSalePrice = await responseSalePrice.Content.ReadAsStringAsync();
            ViewBag.salePrice = messageSalePrice;
            Trace.WriteLine(messageSalePrice);
            TempData["SalePrice"] = messageSalePrice;
            //Trace.WriteLine(TempData["PersonID"] + " - " + TempData["FlightNo"] + " - " + TempData["SalePrice"]);

            Models.Flight flight = JsonConvert.DeserializeObject<Models.Flight>(message);

            return View("Confirm", flight);
        }

        public async Task<ActionResult> ContinueAsync()
        {
            int PersonId = (int)TempData["PersonID"];
            int FlightNo = (int)TempData["FlightNo"];
            double SalePrice = Convert.ToDouble(TempData["SalePrice"]);

            Booking b = new Booking { PassengerID = PersonId, FlightNo = FlightNo, SalePrice = SalePrice};
            string passengerJson = JsonConvert.SerializeObject(b);
            HttpContent stringContent = new StringContent(passengerJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/Bookings", stringContent);
            response.EnsureSuccessStatusCode();

            return View();
        }

    }
}

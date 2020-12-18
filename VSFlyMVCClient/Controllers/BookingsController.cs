using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
            return View();
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
            HttpResponseMessage response = await _httpClient.GetAsync("api/Flights/" + id);
            response.EnsureSuccessStatusCode();
            string message = await response.Content.ReadAsStringAsync();
            Trace.WriteLine(message);
            Models.Flight flight = JsonConvert.DeserializeObject<Models.Flight>(message);
            Trace.WriteLine(flight.FinalPrice);

            return View("Confirm", flight);
        }
    }
}

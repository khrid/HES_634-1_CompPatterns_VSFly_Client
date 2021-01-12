using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VSFlyMVCClient.Models;

namespace VSFlyMVCClient.Controllers
{
    public class PassengersController : Controller
    {
        private static HttpClient _httpClient;

        static PassengersController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44350/");
        }
        // GET: PassengersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PassengersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PassengersController/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: PassengersController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.Passenger p)
        {
            Models.Message m = new Models.Message();
            try
            {
                string passengerJson = JsonConvert.SerializeObject(p);
                HttpContent stringContent = new StringContent(passengerJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Passengers", stringContent);
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                //m.Content = data;
                //return View("ShowMessage", m);
                Models.Passenger passenger = JsonConvert.DeserializeObject<Models.Passenger>(data);
                //Trace.WriteLine(flight.FinalPrice);
                TempData["PersonId"] = passenger.PersonID;
                return RedirectToAction("Continue", "Bookings");
            }
            catch (Exception e)
            {
                m.Content = e.Message;
                return View("ShowMessage", m);
            }
        }

    }
}

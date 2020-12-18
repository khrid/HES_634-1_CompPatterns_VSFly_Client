using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VSFlyMVCClient.Controllers
{
    public class FlightsController : Controller
    {
        private static HttpClient _httpClient;
        // GET: FlightsController

        static FlightsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress= new Uri("https://localhost:44350/");
        }
        public async Task<ActionResult> IndexAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Flights");
            response.EnsureSuccessStatusCode();
            string message = await response.Content.ReadAsStringAsync();

            IEnumerable<Models.Flight> listFlight = JsonConvert.DeserializeObject<IEnumerable<Models.Flight>>(message);

            return View("IndexAsync", listFlight);
        }

        // GET: FlightsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.Flight f)
        {
            Models.Message m= new Models.Message();
            try
            {
                string flightJson = JsonConvert.SerializeObject(f);
                HttpContent stringContent = new StringContent(flightJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Flights", stringContent);
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                m.Content = data;


                return View("ShowMessage", m);
            }
            catch(Exception e)
            {
                m.Content = e.Message;
                return View("ShowMessage", m);
            }
        }

        // GET: FlightsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}

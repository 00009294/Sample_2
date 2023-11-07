using DSCC_9294_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DSCC_9294_MVC.Controllers
{
    public class CarController : Controller
    {
        private Uri baseAddress = new Uri("https://localhost:44327/api");
        private readonly HttpClient httpClient;

        public CarController()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            IList<CarViewModel> carList = new List<CarViewModel>();
            HttpResponseMessage response = this.httpClient.GetAsync(this.httpClient.BaseAddress + "/car/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                carList = JsonConvert.DeserializeObject<List<CarViewModel>>(data);
            }

            return View(carList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarViewModel carViewModel)
        {
            try
            {
                var data = JsonConvert.SerializeObject(carViewModel);
                StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = this.httpClient.PostAsync(
                    this.httpClient.BaseAddress + "/Car/Post", stringContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Car created";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CarViewModel car = new CarViewModel();
            HttpResponseMessage response = this.httpClient.GetAsync(
                this.httpClient.BaseAddress + "/Car/Get/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<CarViewModel>(data);
            }
            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(CarViewModel carViewModel)
        {
            try
            {
                var data = JsonConvert.SerializeObject(carViewModel);
                StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = this.httpClient.PutAsync(
                    this.httpClient.BaseAddress + "/Car/Put", stringContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Car updated";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                CarViewModel car = new CarViewModel();
                HttpResponseMessage response = this.httpClient.GetAsync(
                    this.httpClient.BaseAddress + "/Car/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    car = JsonConvert.DeserializeObject<CarViewModel>(data);
                }
                return View(car);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;

                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = this.httpClient.DeleteAsync(
                    this.httpClient.BaseAddress + "/Car/Delete/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Car deleted";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
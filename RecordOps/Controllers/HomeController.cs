using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecordOps.Models;
using RecordOps.ViewModels;
using System.Diagnostics;

namespace RecordOps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        Uri BaseAddress = new Uri("https://localhost:7066/api");
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = BaseAddress;

        }

        public IActionResult Index()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetCustomers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                // Log the JSON response
                System.Diagnostics.Debug.WriteLine(data);
                customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);


            }
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

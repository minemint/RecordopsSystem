using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecordOps.Models;
using System.Diagnostics;

namespace RecordOps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            var response = _httpClient.GetAsync("https://localhost:5293/api/customer").Result;
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<CustomerModel>());
            }
  
            using (HttpContent content = response.Content)
            {
                var data = content.ReadAsStringAsync().Result;
                var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
                return View(customers);
            }

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

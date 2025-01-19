using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RecordOps.Models;
using RecordOps.ViewModels;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace RecordOps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        Uri BaseAddress = new Uri("https://localhost:7066/api");
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = BaseAddress;
            _hostingEnvironment = hostingEnvironment;

        }

        public IActionResult Index()
        {

            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetCustomers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                List<CustomerViewModel> customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
                return View(customers);

            }
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            //selectlist district and subdistrict
            HttpResponseMessage responseDistricts = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetDistricts").Result;
            HttpResponseMessage responseSubdistricts = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetSubdistricts").Result;
            HttpResponseMessage responseProvinces = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetProvinces").Result;

            if (responseDistricts.IsSuccessStatusCode)
            {
                //relation between district and subdistrict and province
                var data = responseDistricts.Content.ReadAsStringAsync().Result;
                List<CreateViewModel> districts = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);
                SelectList districtList = new SelectList(districts, "districtCode", "districtNameTh");
                ViewBag.Districts = districtList;
            }
            if(responseSubdistricts.IsSuccessStatusCode)
            {
                var data = responseSubdistricts.Content.ReadAsStringAsync().Result;
                List<CreateViewModel> subdistricts = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);
                SelectList subdistrictList = new SelectList(subdistricts, "subdistrictCode", "subdistrictNameTh");
                ViewBag.Subdistricts = subdistrictList;
            }
            if (responseProvinces.IsSuccessStatusCode)
            {
                var data = responseProvinces.Content.ReadAsStringAsync().Result;
                List<CreateViewModel> provinces = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);
                SelectList provinceList = new SelectList(provinces, "provinceCode", "provinceNameTh");
                ViewBag.Provinces = provinceList;
            }
            return View();

        }


        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model, IFormFile? file)
        {
            string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadfolder))
            {
                Directory.CreateDirectory(uploadfolder);
            }
            if (file == null || file.Length == 0)
            {
                string filename = "noimage.jpg";
                var customer = new CreateViewModel
                {
                    //CustomerTitleName = model.CustomerTitleName,
                    //CustomerFName = model.CustomerFName,
                    //CustomerLName = model.CustomerLName,
                    //CustomerAddress = model.CustomerAddress,
                    //CustomerProvince = model.CustomerProvince,
                    //DistrictId = model.DistrictId,
                    //SubdistrictId = model.SubdistrictId,
                    //CustomerPostalCode = model.CustomerPostalCode,
                    //CustomerPhone = model.CustomerPhone.ToString(),
                    //customerImage = filename
                    customerTitleName = model.customerTitleName,
                    customerFName = model.customerFName,
                    customerLName = model.customerLName,
                    customerAddress = model.customerAddress,
                    customerPhone = model.customerPhone,
                    districtCode = model.districtCode,
                    subdistrictCode = model.subdistrictCode,
                    provinceCode = model.provinceCode,
                    customerImage = filename

                };
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(customer);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/AddCustomer", data);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filepath = Path.Combine(uploadfolder, filename);
                using (FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(fs);
                    var customer = new CreateViewModel
                    {
                        //CustomerTitleName = model.CustomerTitleName,
                        //CustomerFName = model.CustomerFName,
                        //CustomerLName = model.CustomerLName,
                        //CustomerAddress = model.CustomerAddress,
                        //CustomerProvince = model.CustomerProvince,
                        //DistrictId = model.DistrictId,
                        //SubdistrictId = model.SubdistrictId,
                        //CustomerPostalCode = model.CustomerPostalCode,
                        //CustomerPhone = model.CustomerPhone.ToString(),
                        //customerImage = filename
                        customerTitleName = model.customerTitleName,
                        customerFName = model.customerFName,
                        customerLName = model.customerLName,
                        customerAddress = model.customerAddress,
                        customerPhone = model.customerPhone,
                        districtCode = model.districtCode,
                        subdistrictCode = model.subdistrictCode,
                        provinceCode = model.provinceCode,
                        customerImage = filename
                    };

                    if (ModelState.IsValid)
                    {
                        var json = JsonConvert.SerializeObject(customer);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/AddCustomer", data);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }

                }
            }

            return View(model);
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {

            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetCustomer/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var customer = JsonConvert.DeserializeObject<EditViewModel>(data);
                return View(customer);
            }
            return RedirectToAction("Index");
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, IFormFile? file)
        {
            string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filepath = Path.Combine(uploadfolder, filename);
                using (FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(fs);
                    var customer = new CustomerViewModel
                    {
                        //CustomerId = Convert.ToInt32(collection["CustomerId"]),
                        //CustomerFName = collection["CustomerFName"],
                        //CustomerLName = collection["CustomerLName"],
                        //CustomerAddress = collection["CustomerAddress"],
                        //DistrictId = Convert.ToInt32(collection["DistrictId"]),
                        //SubdistrictId = Convert.ToInt32(collection["SubdistrictId"]),
                        //CustomerPostalCode = collection["CustomerPostalCode"],
                        //CustomerPhone = collection["CustomerPhone"],
                        //customerImage = filename
                    };
                    if (ModelState.IsValid)
                    {
                        var json = JsonConvert.SerializeObject(customer);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        System.Console.WriteLine(json);
                        HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + $"/Customer/UpdateCustomer/{id}", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "เกิดข้อผิดพลาดในการอัปเดตข้อมูล");
                        }
                    }
                }

            }
            else
            {
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetCustomer/{id}").Result;
                var data = response.Content.ReadAsStringAsync().Result;
                var customer2 = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                var customer = new CustomerViewModel
                {
                    //CustomerId = Convert.ToInt32(collection["CustomerId"]),
                    //CustomerTitleName = collection["CustomerTitleName"],
                    //CustomerFName = collection["CustomerFName"],
                    //CustomerLName = collection["CustomerLName"],
                    //CustomerAddress = collection["CustomerAddress"],
                    //DistrictId = Convert.ToInt32(collection["DistrictId"]),
                    //SubdistrictId = Convert.ToInt32(collection["SubdistrictId"]),
                    //CustomerProvince = collection["CustomerProvince"],
                    //CustomerPostalCode = (collection["CustomerPostalCode"]),
                    //CustomerPhone = collection["CustomerPhone"],
                    //customerImage = Convert.ToString(customer2.customerImage)
                };


                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(customer);
                    var data2 = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response2 = _httpClient.PutAsync(_httpClient.BaseAddress + $"/Customer/UpdateCustomer/{id}", data2).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "เกิดข้อผิดพลาดในการอัปเดตข้อมูล");
                    }
                }
            }
            return View();
        }




        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetCustomer/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                return View(customer);
            }
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/Customer/DeleteCustomer/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "เกิดข้อผิดพลาดในการลบข้อมูล");
            }
            return View();
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
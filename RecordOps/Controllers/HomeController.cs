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

        public async Task<IActionResult> Index(int dcode, int subcode)
        {
            var provincesResult = GetProvinces() as JsonResult;
            System.Console.WriteLine(provincesResult);
            ViewBag.Provinces = provincesResult.Value;
            System.Console.WriteLine(provincesResult.Value);
            var districtsResult =await Getdistrict(dcode) as JsonResult;
            ViewBag.Districts = districtsResult.Value;

            var subdistrictsResult = await Getsubdistrict(subcode) as JsonResult;
            ViewBag.Subdistricts = subdistrictsResult.Value;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetCustomers").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                List<CustomerViewModel> customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
                //customers.
                return View(customers);

            }
            else
            {
                return Redirect("Home");

            }
        }
        public IActionResult Home()
        {
            return View();
        }

        //GET: HomeController1/Create
        public async Task<IActionResult> Create(int dcode, int subcode)
        {
            var provincesResult = GetProvinces() as JsonResult;
            System.Console.WriteLine(provincesResult);
            ViewBag.Provinces = provincesResult.Value;
            System.Console.WriteLine(provincesResult.Value);
            ////เรียกใช้ฟังก์ชัน GetDistrict และส่งค่า province ไปด้วย
            var districtsResult =await Getdistrict(dcode) as JsonResult;
            ////// เก็บค่าที่ได้จากการเรียกใช้ฟังก์ชัน GetDistrict ไว้ใน ViewBag.Districts
            ViewBag.Districts = districtsResult.Value;

            var subdistrictsResult = await Getsubdistrict(subcode) as JsonResult;
            ViewBag.Subdistricts = subdistrictsResult.Value;
            return View();

        }

        // POST: HomeController/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateViewModel model, IFormFile? file)
        {
            Console.WriteLine($"Received Data: {JsonConvert.SerializeObject(model)}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model State is Invalid!");
                return View(model);
            }

            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string filename = file != null && file.Length > 0
                ? Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                : "noimage.jpg";

            if (file != null && file.Length > 0)
            {
                string filePath = Path.Combine(uploadFolder, filename);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }

            var customer = new CreateViewModel
            {
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

            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/AddCustomer", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult GetProvinces()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetProvinces").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                List<CreateViewModel> provinces = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);
                SelectList provinceList = new SelectList(provinces, "provinceCode", "provinceNameTh");
                return Json(provinceList);
            }
            return Json(null);
        }
        [HttpGet]
        public async Task<IActionResult> GetProvince(int provinceCode)
        {
            try
            {
                HttpResponseMessage response;
                List<CreateViewModel> provinces;

                if (provinceCode != 0)
                {
                    response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetProvinceWithProvinceCode/{provinceCode}");
                }
                else
                {
                    response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetProvinces");
                }

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    provinces = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);
                    var provinceList = new SelectList(provinces, "provinceCode", "provinceNameTh");
                    return Json(provinceList); // ส่งกลับข้อมูลในรูปแบบ JSON
                }

                return Json(new { error = "Failed to retrieve provinces." });
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Getdistrict(int provinceCode)
        {
            try
            {
                HttpResponseMessage response;
                List<CreateViewModel> districts;

                if (provinceCode != 0)
                {
                    response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetDistrictsWithProvince/{provinceCode}");
                }
                else
                {
                    response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetDistricts");
                }

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    districts = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);
                    var districtList = new SelectList(districts, "districtCode", "districtNameTh");
                    return Json(districtList); // ส่งกลับข้อมูลในรูปแบบ JSON
                }

                return Json(new { error = "Failed to retrieve districts." });
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Getsubdistrict(int districtCode)
        {
            try
            {
                HttpResponseMessage response;
                List<CreateViewModel> subdistricts;

                if (districtCode != 0)
                {
                    response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetsubdistrictsWithDistrict/{districtCode}");
                }
                else
                {
                    response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/GetSubdistricts");
                }

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    subdistricts = JsonConvert.DeserializeObject<List<CreateViewModel>>(data);

                    var subdistrictList = new SelectList(subdistricts, "subdistrictCode", "subdistrictNameTh");
                    return Json(subdistrictList);
                }
                return Json(new { error = "Failed to retrieve subdistricts." });
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }

        // GET: HomeController1/Edit/5
        public async Task<IActionResult>  Edit(int id)
        {
           
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Customer/GetCustomer/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var customer = JsonConvert.DeserializeObject<EditViewModel>(data);
                var provincesResult = GetProvinces() as JsonResult;
                System.Console.WriteLine(provincesResult);
                ViewBag.Provinces = provincesResult.Value;
                var districtsResult = await Getdistrict(Convert.ToInt32(customer.provinceCode)) as JsonResult;
                ViewBag.Districts = districtsResult.Value;
                var subdistrictsResult = await Getsubdistrict(Convert.ToInt32(customer.districtCode)) as JsonResult;
                ViewBag.SubDistricts = subdistrictsResult.Value;
                return View(customer);
            }
            return RedirectToAction("Index");
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, IFormFile? file)
        {
            try
            {
                string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                // ตรวจสอบว่าโฟลเดอร์ uploads มีอยู่หรือไม่ ถ้าไม่มีให้สร้าง
                if (!Directory.Exists(uploadfolder))
                {
                    Directory.CreateDirectory(uploadfolder);
                }

                string filename = "noimage.jpg"; // กำหนดค่าเริ่มต้น
                if (file != null && file.Length > 0)
                {
                    filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filepath = Path.Combine(uploadfolder, filename);

                    // บันทึกไฟล์แบบ async
                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }
                }

                var customer = new EditViewModel
                {
                    customerId = Convert.ToInt32(collection["CustomerId"]),
                    customerFName = collection["CustomerFName"],
                    customerLName = collection["CustomerLName"],
                    provinceCode = Convert.ToInt32(collection["ProvinceCode"]),
                    customerAddress = collection["CustomerAddress"],
                    districtCode = Convert.ToInt32(collection["DistrictCode"]),
                    subdistrictCode = Convert.ToInt32(collection["SubdistrictCode"]),
                    customerPhone = collection["CustomerPhone"],
                    customerImage = filename
                };

                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(customer);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    System.Console.WriteLine(json);

                    HttpResponseMessage response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Customer/UpdateCustomer/{id}", data);

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
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"เกิดข้อผิดพลาด: {ex.Message}");
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
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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = BaseAddress;

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
            ViewBag.Districts = new SelectList(GetDistricts(), "DistrictId", "DistrictName");
            ViewBag.Subdistricts = new SelectList(GetSubdistricts(), "SubdistrictId", "SubdistrictName");
            var viewModel = new CreateViewModel
            {
                Districts = ViewBag.Districts,
                Subdistricts = ViewBag.Subdistricts

            };
            return View(viewModel);
        }

        private IEnumerable GetSubdistricts()
        {
            var subdistricts = new List<SubdistrictModel>
            {
                new SubdistrictModel { subdistrictId = 1, subdistrictName = "A 1" },
                new SubdistrictModel { subdistrictId = 2, subdistrictName = "B 2" },
                new SubdistrictModel { subdistrictId = 3, subdistrictName = "C 3" }
            };
            return subdistricts;
        }

        private IEnumerable GetDistricts()
        {
            var districts = new List<DistrictModel>
            {
                new DistrictModel { districtId = 1, districtName = "A" },
                new DistrictModel { districtId = 2, districtName = "B" },
                new DistrictModel { districtId = 3, districtName = "C" }
            };
            return districts;
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(model);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/AddCustomer", data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
            ViewBag.Districts = new SelectList(GetDistricts(), "DistrictId", "DistrictName", customer.DistrictId);
            ViewBag.Subdistricts = new SelectList(GetSubdistricts(), "SubdistrictId", "SubdistrictName", customer.SubdistrictId);
            return View(customer);
            }
            return RedirectToAction("Index");
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            // ตรวจสอบข้อมูลจากฟอร์ม
            if (ModelState.IsValid)
            {
                try
                {
                    // สร้าง EditViewModel จากข้อมูลที่รับมาใน form
                    var customer = new EditViewModel
                    {
                        CustomerId = Convert.ToInt32(collection["CustomerId"]), // รับข้อมูลชื่อ
                        CustomerFName = collection["CustomerFName"], // รับข้อมูลชื่อ
                        CustomerLName = collection["CustomerLName"], // รับข้อมูลนามสกุล
                        CustomerAddress = collection["CustomerAddress"], // รับข้อมูลที่อยู่
                        DistrictId = Convert.ToInt32(collection["DistrictId"]), // รับข้อมูลรหัสเขต
                        SubdistrictId = Convert.ToInt32(collection["SubdistrictId"]), // รับข้อมูลรหัสตำบล
                        CustomerPostalCode = collection["CustomerPostalCode"], // รับข้อมูลรหัสไปรษณีย์
                        CustomerPhone = collection["CustomerPhone"] // รับข้อมูลเบอร์โทร
                    };

                    // ส่งข้อมูลไปยัง API เพื่ออัปเดตลูกค้า มีid และข้อมูลลูกค้า
                    var json = JsonConvert.SerializeObject(customer);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + $"/Customer/UpdateCustomer/{id}", data).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        // หากการอัปเดตสำเร็จ ให้ redirect ไปที่หน้าหลัก (Index)
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // หากการอัปเดตล้มเหลว ให้แสดง error message หรือข้อผิดพลาด
                        ModelState.AddModelError("", "เกิดข้อผิดพลาดในการอัปเดตข้อมูล");
                    }
                }
                catch (Exception ex)
                {
                    // หากมีข้อผิดพลาด ให้แสดงข้อผิดพลาดใน ModelState
                    ModelState.AddModelError("", $"เกิดข้อผิดพลาด: {ex.Message}");
                }
            }

            // หากข้อมูลไม่ถูกต้อง หรือการอัปเดตล้มเหลว ให้กลับไปที่ View และแสดงผลลัพธ์เดิม
            return View();
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
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
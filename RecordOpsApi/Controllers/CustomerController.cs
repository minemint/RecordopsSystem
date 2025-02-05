using Microsoft.AspNetCore.Mvc;
using RecordOpsApi.Models;
using RecordOpsApi.Repositories.Interfaces;

namespace RecordOpsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private string responseMessage;


        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

        }

        [HttpGet]
        [Route("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomers();
            if (customers == null)
            {
                responseMessage = "ไม่พบข้อมูลลูกค้า";
                return BadRequest(responseMessage);
            }
            return Ok(customers);
        }

        [HttpGet]
        [Route("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {

            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                responseMessage = $"ไม่พบข้อมูลลูกค้า id:  {id}";
                return BadRequest(responseMessage);
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] MCustomer customer)
        {
            var newCustomer = await _customerRepository.AddCustomer(customer);
            if (newCustomer == null)
            {
                responseMessage = "ไม่สามารถเพิ่มข้อมูลลูกค้า";
                return BadRequest(responseMessage);
            }
            return Ok(newCustomer);
        }

        [HttpPut]
        [Route("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] MCustomer customer,int id)
        {
            var customerToUpdate = await _customerRepository.GetCustomer(id);
            if(customerToUpdate == null)
            {
                responseMessage = "ไม่พบข้อมูลลูกค้า";
                return BadRequest(responseMessage);
            }
            else
            {
                customer.customerId = id;
                var updatedCustomer = await _customerRepository.UpdateCustomer(customer);
                if (updatedCustomer == null)
                {
                    responseMessage = "ไม่สามารถแก้ไขข้อมูลลูกค้าได้";
                    return BadRequest(responseMessage);
                }
                return Ok(updatedCustomer);

            }
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        { 
            if(id == 0)
            {
                responseMessage = "ไม่พบข้อมูลลูกค้า";
                return BadRequest(responseMessage);
            }
            else
            {
                _customerRepository.DeleteCustomer(id);
                return Ok("ลบข้อมูลลูกค้าเรียบร้อยแล้ว");
            }
        }
        [HttpGet]
        [Route("GetCustomersWithStoreP")]
        public async Task<IActionResult> GetCustomersWithStoreP()
        {
            var customers = await _customerRepository.GetCustomersWithStoreP();
            if (customers == null)
            {
                responseMessage = "ไม่พบข้อมูลลูกค้า";
                return BadRequest(responseMessage);
            }
            return Ok(customers);
        }
        [HttpGet]
        [Route("GetProvinces")]
        public async Task<IActionResult> GetProvinces()
        {
            var provinces = await _customerRepository.GetProvinces();
            if (provinces == null)
            {
                responseMessage = "ไม่พบข้อมูลจังหวัด";
                return BadRequest(responseMessage);
            }
            return Ok(provinces);
        }
        [HttpGet]
        [Route("GetDistricts")]
        public async Task<IActionResult> GetDistricts()
        {
            var districts = await _customerRepository.GetDistricts();
            if (districts == null)
            {
                responseMessage = "ไม่พบข้อมูลอำเภอ";
                return BadRequest(responseMessage);
            }
            return Ok(districts);
        }
        [HttpGet]
        [Route("GetSubdistricts")]
        public async Task<IActionResult> GetSubdistricts()
        {
            var subdistricts = await _customerRepository.GetSubdistricts();
            if (subdistricts == null)
            {
                responseMessage = "ไม่พบข้อมูลตำบล";
                return BadRequest(responseMessage);
            }
            return Ok(subdistricts);
        }
        [HttpGet]
        [Route("GetProvinceWithProvinceCode/{id}")]
        public async Task<IActionResult> GetProvinceWithProvinceCode(int id)
        {
            var districts = await _customerRepository.GetProvinceWithProvinceCode(id);
            if (districts == null)
            {
                responseMessage = "ไม่พบข้อมูลจังหวัด";
                return BadRequest(responseMessage);
            }
            return Ok(districts);

        }
        [HttpGet]
        [Route("GetDistrictsWithProvince/{id}")]
        public async Task<IActionResult> GetdistrictsWithProvince(int id)
        {
            var districts = await _customerRepository.GetDistrictsWithProvince(id);
            if (districts == null)
            {
                responseMessage = "ไม่พบข้อมูลแขวง";
                return BadRequest(responseMessage);
            }
            return Ok(districts);

        }
        [HttpGet]
        [Route("GetSubdistrictsWithDistrict/{id}")]
        public async Task<IActionResult> GetSubdistrictsWithDistrict(int id)
        {
            var subdistricts = await _customerRepository.GetSubdistrictsWithDistrict(id);
            if (subdistricts == null)
            {
                responseMessage = "ไม่พบข้อมูลตำบล";
                return BadRequest(responseMessage);
            }
            return Ok(subdistricts);
        }
        
    }
}

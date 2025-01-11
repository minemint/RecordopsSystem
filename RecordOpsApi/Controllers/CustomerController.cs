using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecordOpsApi.Models;
using RecordOpsApi.Repositories;
using RecordOpsApi.Repositories.Interfaces;

namespace RecordOpsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;


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
                return NotFound();
            }
            return Ok(customers);
        }

        [HttpGet]
        [Route("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {   
         
            var customer =  await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] MCustomer customer , IFormFile? image)
        {
            var newCustomer = await _customerRepository.AddCustomer(customer,image);
           
            return Ok(newCustomer);
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] MCustomer customer , int id, IFormFile? image)
        {
            var updatedCustomer = await _customerRepository.UpdateCustomer(customer, id,image);

            return Ok(updatedCustomer);
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerRepository.DeleteCustomer(id);
            return Ok();
        }
    }
}

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

            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] MCustomer customer)
        {
            var newCustomer = await _customerRepository.AddCustomer(customer);

            return Ok(newCustomer);
        }

        [HttpPut]
        [Route("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] MCustomer customer)
        {
            var updatedCustomer = await _customerRepository.UpdateCustomer(customer);

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

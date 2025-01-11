using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RecordOpsApi.Models;
using RecordOpsApi.Repositories.Interfaces;

namespace RecordOpsApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RecordOpsDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public CustomerRepository(RecordOpsDbContext option, IMemoryCache memoryCache)
        {
            _context = option;
            _memoryCache = memoryCache;

        }

        public async Task<MCustomer> AddCustomer(MCustomer customer)
        {
            _context.customer_tbl.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public void DeleteCustomer(int id)
        {
            _context.customer_tbl.Remove(_context.customer_tbl.Find(id));
        }

        public async Task<MCustomer> GetCustomer(int id)
        {
            var customer = _context.customer_tbl.Find(id);
            return customer;
        }

        public async Task<IEnumerable<MCustomer>> GetCustomers()
        {
            // Include the navigation property (District) instead of districtId
            var customers = await _context.customer_tbl
                                           .Include(x => x.districtId)  // Correctly include the navigation property
                                           .ToListAsync();
            return customers;
        }

        public async Task<MCustomer> UpdateCustomer(MCustomer customer)
        {
            var customerToUpdate = _context.customer_tbl.Find(customer.customerId);
            customerToUpdate.customerTitleName = customer.customerTitleName;
            customerToUpdate.customerFName = customer.customerFName;
            customerToUpdate.customerLName = customer.customerLName;
            customerToUpdate.customerAddress = customer.customerAddress;
            customerToUpdate.customerPhone = customer.customerPhone;
            customerToUpdate.customerImage = customer.customerImage;

            await _context.SaveChangesAsync();
            return customerToUpdate;
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
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


        public async Task<MCustomer> AddCustomer(MCustomer customer, IFormFile? image)
        {

            var newCustomer = new MCustomer
            {
                customerTitleName = customer.customerTitleName,
                customerFName = customer.customerFName,
                customerLName = customer.customerLName,
                customerAddress = customer.customerAddress,
                customerPhone = customer.customerPhone,
                customerImage = customer.customerImage,
                districtId = customer.districtId,
                subdistrictId = customer.subdistrictId,
                customerPostalCode = customer.customerPostalCode
            };           
            _context.customer_tbl.Add(newCustomer);

            if (image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                string uploadFolder = Path.Combine("wwwroot","Images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                newCustomer.customerImage = fileName;

            }
            else
            {
                newCustomer.customerImage = "noimg.jpg";
            }

            await _context.SaveChangesAsync();
            return newCustomer;
        }

        public void DeleteCustomer(int id)
        {
            var customerToDelete = _context.customer_tbl.Find(id);
            _context.customer_tbl.Remove(customerToDelete);
            _context.SaveChanges();
        }

        public async Task<MCustomer> GetCustomer(int id)
        {
            var customer = await _context.customer_tbl.Include(c => c.district).ThenInclude(d => d.Subdistricts).FirstOrDefaultAsync(c => c.customerId == id);
            return customer;
        }

        public async Task<IEnumerable<MCustomer>> GetCustomers()
        {
            var customers = await _context.customer_tbl.Include(c => c.district).ThenInclude(d => d.Subdistricts).ToListAsync();
            return customers;
        }


        public async Task<MCustomer> UpdateCustomer(MCustomer customer, int id, IFormFile? image)
        {
            var oldcustomer = _context.customer_tbl.Include(c => c.district).ThenInclude(d => d.Subdistricts).FirstOrDefault(c => c.customerId == id);

            oldcustomer.customerTitleName = customer.customerTitleName;
            oldcustomer.customerFName = customer.customerFName;
            oldcustomer.customerLName = customer.customerLName;
            oldcustomer.customerAddress = customer.customerAddress;
            oldcustomer.customerPhone = customer.customerPhone;
            oldcustomer.districtId = customer.districtId;
            oldcustomer.subdistrictId = customer.subdistrictId;
            oldcustomer.customerPostalCode = customer.customerPostalCode;

            if (image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                string uploadFolder = Path.Combine("wwwroot","Images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                if (oldcustomer.customerImage != "noimg.jpg")
                {
                    System.IO.File.Delete(Path.Combine(uploadFolder, oldcustomer.customerImage!));
                }

                oldcustomer.customerImage = fileName;
            }

            await _context.SaveChangesAsync();
            return oldcustomer;
        }
    }
}

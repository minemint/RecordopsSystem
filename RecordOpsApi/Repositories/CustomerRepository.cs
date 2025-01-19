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

            try
            {
                var newCustomer = new MCustomer
                {
                    customerTitleName = customer.customerTitleName,
                    customerFName = customer.customerFName,
                    customerLName = customer.customerLName,
                    customerAddress = customer.customerAddress,
                    provinceCode = customer.provinceCode,
                    customerPhone = customer.customerPhone,
                    customerImage = customer.customerImage,
                    districtCode = customer.districtCode,
                    subdistrictCode = customer.subdistrictCode,
                };
                _context.customer_tbl.Add(newCustomer);

                await _context.SaveChangesAsync();
                return newCustomer;
            }
            catch (Exception ex)
            {
                // แสดงข้อผิดพลาดใน Console หรือใช้ Logging Framework เช่น Serilog
                Console.WriteLine($"เกิดข้อผิดพลาดขณะเพิ่มข้อมูลลูกค้า: {ex.Message}");
                throw new Exception("ไม่สามารถเพิ่มข้อมูลลูกค้าได้", ex);
            }
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                var customerToDelete = _context.customer_tbl.Find(id);
                _context.customer_tbl.Remove(customerToDelete);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // แสดงข้อผิดพลาดใน Console หรือใช้ Logging Framework เช่น Serilog
                Console.WriteLine($"เกิดข้อผิดพลาดขณะลบข้อมูลลูกค้า: {ex.Message}");
                throw new Exception("ไม่สามารถลบข้อมูลลูกค้าได้", ex);
            }
        }

        public async Task<MCustomer> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.customer_tbl.Include(c => c.district).Include(c => c.Subdistrict).Include(c => c.Province).FirstOrDefaultAsync(c => c.customerId == id);
                if (customer == null)
                {
                    throw new Exception("ไม่พบข้อมูลลูกค้า");
                }
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดขณะดึงข้อมูลลูกค้า: {ex.Message}");
                throw new Exception("ไม่สามารถดึงข้อมูลลูกค้าได้", ex);
            }
        }

        public async Task<IEnumerable<MCustomer>> GetCustomers()
        {
            try
            {
                var customers = await _context.customer_tbl.Include(c => c.district).Include(c => c.Subdistrict).Include(c => c.Province).ToListAsync();
                return customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดขณะดึงข้อมูลลูกค้า: {ex.Message}");
                throw new Exception("ไม่สามารถดึงข้อมูลลูกค้าได้", ex);

            }
        }

        public async Task<IEnumerable<MCustomer>> GetCustomersWithStoreP()
        {
            try
            {
                var customers = _memoryCache.Get<IEnumerable<MCustomer>>("Customers");
                if (customers == null)
                {
                    //store procedure
                    customers = await _context.customer_tbl.FromSqlRaw("CALL GetCustomers").ToListAsync();
                    _memoryCache.Set("Customers", customers, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
                return customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดขณะดึงข้อมูลลูกค้า: {ex.Message}");
                throw new Exception("ไม่สามารถดึงข้อมูลลูกค้าได้", ex);
            }
        }

        public async Task<IEnumerable<MDistrict>> GetDistricts()
        {
            try
            {
                var districts = _memoryCache.Get<IEnumerable<MDistrict>>("Districts");
                if (districts == null)
                {
                    //store procedure
                    districts = await _context.district_tbl.ToListAsync();
                    _memoryCache.Set("Districts", districts, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
                return districts;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดขณะดึงข้อมูลอำเภอ: {ex.Message}");
                throw new Exception("ไม่สามารถดึงข้อมูลอำเภอได้", ex);
            }
        }

        public async Task<IEnumerable<MDistrict>> GetDistrictsWithProvince(int id)
        {
            var districts = _memoryCache.Get<IEnumerable<MDistrict>>("Districts");
            if (districts == null)
            {
                districts = await _context.district_tbl.Include(d => d.province).Where(d => d.provinceCode == id).ToListAsync();
                _memoryCache.Set("Districts", districts, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            return districts;
        }

        public async Task<IEnumerable<MProvince>> GetProvinces()
        {
            try
            {
                var provinces = _memoryCache.Get<IEnumerable<MProvince>>("Provinces");
                if (provinces == null)
                {
                    //store procedure
                    provinces = await _context.province_tbl.ToListAsync();
                    _memoryCache.Set("Provinces", provinces, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
                return provinces;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดขณะดึงข้อมูลจังหวัด: {ex.Message}");
                throw new Exception("ไม่สามารถดึงข้อมูลจังหวัดได้", ex);
            }
        }
        public async Task<IEnumerable<MSubdistrict>> GetSubdistricts()
        {
            try
            {
                var subdistricts = _memoryCache.Get<IEnumerable<MSubdistrict>>("Subdistricts");
                if (subdistricts == null)
                {
                    subdistricts = await _context.subdistrict_tbl.ToListAsync();
                    _memoryCache.Set("Subdistricts", subdistricts, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
                return subdistricts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดขณะดึงข้อมูลอำเภอ: {ex.Message}");
                throw new Exception("ไม่สามารถดึงข้อมูลอำเภอได้", ex);
            }
        }

        public async Task<IEnumerable<MSubdistrict>> GetSubdistrictsWithDistrict(int id)
        {
            var subdistricts = _memoryCache.Get<IEnumerable<MSubdistrict>>("Subdistricts");
            if (subdistricts == null)
            {
                subdistricts = await _context.subdistrict_tbl.Include(s => s.district).Where(s => s.districtCode == id).ToListAsync();
                _memoryCache.Set("Subdistricts", subdistricts, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            return subdistricts;

        }

        public async Task<MCustomer> UpdateCustomer(MCustomer customer)
        {
            try
            {
                var oldcustomer = _context.customer_tbl.FirstOrDefault(c => c.customerId == customer.customerId);
                oldcustomer.customerTitleName = oldcustomer.customerTitleName;
                oldcustomer.customerFName = customer.customerFName;
                oldcustomer.customerLName = customer.customerLName;
                oldcustomer.customerAddress = customer.customerAddress;
                oldcustomer.provinceCode = customer.provinceCode;
                oldcustomer.customerPhone = customer.customerPhone;
                oldcustomer.districtCode = customer.districtCode;
                oldcustomer.subdistrictCode = customer.subdistrictCode;
                oldcustomer.customerImage = customer.customerImage;

                _context.customer_tbl.Update(oldcustomer);

                await _context.SaveChangesAsync();
                return oldcustomer;
            }
            catch (Exception ex)
            {
                // แสดงข้อผิดพลาดใน Console หรือใช้ Logging Framework เช่น Serilog
                Console.WriteLine($"เกิดข้อผิดพลาดขณะอัพเดทข้อมูลลูกค้า: {ex.Message}");
                throw new Exception("ไม่สามารถอัพเดทข้อมูลลูกค้าได้", ex);
            }
        }
    }
}

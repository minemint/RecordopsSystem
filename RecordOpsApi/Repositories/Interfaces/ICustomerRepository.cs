using RecordOpsApi.Models;

namespace RecordOpsApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<MCustomer>> GetCustomers();
        Task<MCustomer> GetCustomer(int id);
        Task<MCustomer> AddCustomer(MCustomer customer);
        Task<MCustomer> UpdateCustomer(MCustomer customer);
        void DeleteCustomer(int id);
        Task<IEnumerable<MCustomer>> GetCustomersWithStoreP();
        Task<IEnumerable<MProvince>> GetProvinces();
        Task<IEnumerable<MDistrict>> GetDistricts();
        Task<IEnumerable<MSubdistrict>> GetSubdistricts();
        Task<IEnumerable<MProvince>> GetProvinceWithProvinceCode(int id);
        Task<IEnumerable<MDistrict>> GetDistrictsWithProvince(int id);
        Task<IEnumerable<MSubdistrict>> GetSubdistrictsWithDistrict(int id);


    }
}

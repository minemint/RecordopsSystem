using RecordOpsApi.Models;

namespace RecordOpsApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<MCustomer>> GetCustomers();
        Task<MCustomer> GetCustomer(int id);
        Task<MCustomer> AddCustomer(MCustomer customer,IFormFile? image);
        Task<MCustomer> UpdateCustomer(MCustomer customer, int id,IFormFile? image);
        void DeleteCustomer(int id);
    }
}

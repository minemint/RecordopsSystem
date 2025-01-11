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
    }
}

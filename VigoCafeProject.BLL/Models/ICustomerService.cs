using DAL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        Customer Register(string fullName, string email, string password, string phone, string address);
        Customer Login(string email, string password);
        bool EmailExists(string email);
        Customer GetById(int id);
        void UpdateProfile(Customer customer);
    }
}
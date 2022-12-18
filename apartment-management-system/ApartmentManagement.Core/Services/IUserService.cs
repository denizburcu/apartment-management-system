using ApartmentManagement.Core.Models;


namespace ApartmentManagement.Core.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserIncludeApartment(string userId);
        Task<IEnumerable<User>> GetAllNonResidentUsers();
    }
}

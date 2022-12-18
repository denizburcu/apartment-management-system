using ApartmentManagement.Core.Models;


namespace ApartmentManagement.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUserIncludeApartment(string userId);
        Task<IEnumerable<User>> GetAllNonResidentUsers();
    }
}

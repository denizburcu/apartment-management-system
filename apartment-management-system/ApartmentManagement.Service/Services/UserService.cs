using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ApartmentManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;


        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserIncludeApartment(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Apartment).FirstOrDefaultAsync();
            return user;
        }


        public async Task<IEnumerable<User>> GetAllNonResidentUsers()
        {
            return await _userManager.Users.Where(x => x.Apartment == null).ToListAsync();
        }
    }
}

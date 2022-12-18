using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagement.Repository.Repositories
{
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Apartment>> GetAllIncludeUserAsync()
        {
            return await _dbSet.AsNoTracking().Include(x => x.User).ToListAsync();
        }
    }
}

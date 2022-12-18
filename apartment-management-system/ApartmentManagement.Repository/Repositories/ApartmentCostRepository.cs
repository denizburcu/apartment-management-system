using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagement.Repository.Repositories
{
    public class ApartmentCostRepository : GenericRepository<ApartmentCost>, IApartmentCostRepository
    {
        public ApartmentCostRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllByIsPaidIncludeApartmentAsync(bool isPaid)
        {
            return await _dbSet.AsNoTracking().Where(a => a.IsPaid == isPaid).Include(a => a.Apartment).ToListAsync();
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllByUserId(string userId)
        {
            return await _dbSet.AsNoTracking().Where(x => x.Apartment.User.Id == userId).Include(x => x.Apartment).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllEmailsByNotPaidApartmentCostsAsync()
        {
            return await _dbSet.AsNoTracking().Where(x => !x.IsPaid).Select(a => a.Apartment).Select(u => u.User.Email).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllNotPaidCostsByMonthIncludeApartmentAsync(Month month)
        {
            return await _dbSet.AsNoTracking().Where(x => !x.IsPaid && x.Month == month).Include(a => a.Apartment).ToListAsync();
        }

        public async Task<IEnumerable<ApartmentCost>> GetAllPaidOrderByDescendingAsync()
        {
            return await _dbSet.AsNoTracking().Where(x => x.IsPaid).Include(z => z.Apartment).OrderByDescending(x => x.CreatedTime).ToListAsync();
        }

        public async Task<ApartmentCost> GetByIdIncludeAparmentAsync(int id)
        {
            return await _dbSet.Where(m => m.Id == id).Include(u => u.Apartment).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}

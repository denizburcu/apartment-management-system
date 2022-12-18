using ApartmentManagement.Core.IUnitOfWorks;

namespace ApartmentManagement.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Clear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}

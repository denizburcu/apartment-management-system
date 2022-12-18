using ApartmentManagement.Core.Models;

namespace ApartmentManagement.Core.Repositories
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Task<IEnumerable<Apartment>> GetAllIncludeUserAsync();
    }
}

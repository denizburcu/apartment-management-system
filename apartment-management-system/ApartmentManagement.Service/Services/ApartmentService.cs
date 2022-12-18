using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.Helpers;
using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;


namespace ApartmentManagement.Service.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ApartmentService(IApartmentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddApartment(Apartment apartment)
        {
            await _repository.AddAsync(apartment);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveApartment(int id)
        {
            var deletedApartment = await _repository.GetByIdAsync(id);
            if (deletedApartment != null)
            {
                _repository.Remove(deletedApartment);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Apartment>> GetAll()
        {
            return await _repository.GetAllIncludeUserAsync();
        }

        public async Task UpdateApartment(Apartment apartment)
        {
            if (apartment.UserId != null)
                apartment.Status = Status.FULL;
            else
                apartment.Status = Status.EMPTY;

            _repository.Update(apartment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Apartment> GetById(int id)
        {
            var apartment = await _repository.GetByIdAsync(id);
            if (apartment == null)
                throw new NotFoundException("Apartment dairesi bulunamadi.");
            return apartment;
        }
    }
}

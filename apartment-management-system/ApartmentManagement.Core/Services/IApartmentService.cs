﻿using ApartmentManagement.Core.Models;

namespace ApartmentManagement.Core.Contracts
{
    public interface IApartmentService
    {
        Task AddApartment(Apartment apartment);
        Task RemoveApartment(int id);  
        Task UpdateApartment(Apartment apartment);
        Task<Apartment> GetById(int id);
        Task<IEnumerable<Apartment>> GetAll();
    }
}

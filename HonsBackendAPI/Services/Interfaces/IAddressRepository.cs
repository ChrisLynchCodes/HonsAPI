﻿using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Repositories
{
    public interface IAddressRepository
    {
        Task CreateAsync(Address newAddress);
        Task<List<Address>> GetAllAsync(string customerId);
        Task<Address?> GetOneAsync(string addressId);
        Task RemoveAsync(string addresId);
        Task UpdateAsync(string addressId, Address updatedAddress);
    }
}
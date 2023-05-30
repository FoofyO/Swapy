﻿using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IElectronicAttributeRepository : IAttributeRepository<ElectronicAttribute>
    {
        Task<ElectronicAttribute> GetByProductIdAsync(string productId);
    }
}

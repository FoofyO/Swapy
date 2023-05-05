﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TransmissionTypeRepository : ITransmissionTypeRepository
    {
        private readonly SwapyDbContext _context;

        public TransmissionTypeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TransmissionType item)
        {
            await _context.TransmissionTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransmissionType item)
        {
            _context.TransmissionTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TransmissionType item)
        {
            _context.TransmissionTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<TransmissionType> GetByIdAsync(Guid id)
        {
            var item = await _context.TransmissionTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TransmissionType>> GetAllAsync()
        {
            return await _context.TransmissionTypes.ToListAsync();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly SwapyDbContext _context;

        public MemoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Memory item)
        {
            await _context.Memories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Memory item)
        {
            _context.Memories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Memory item)
        {
            _context.Memories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Memory> GetByIdAsync(Guid id)
        {
            var item = await _context.Memories.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Memory>> GetAllAsync()
        {
            return await _context.Memories.ToListAsync();
        }
    }
}

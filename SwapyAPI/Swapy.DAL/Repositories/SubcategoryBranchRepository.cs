using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubcategoryBranchRepository : ISubcategoryBranchRepository
    {
        private readonly SwapyDbContext _context;

        public SubcategoryBranchRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(SubcategoryBranch item)
        {
            await _context.SubcategoryBranches.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubcategoryBranch item)
        {
            _context.SubcategoryBranches.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SubcategoryBranch item)
        {
            _context.SubcategoryBranches.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<SubcategoryBranch> GetByIdAsync(string id)
        {
            var item = await _context.SubcategoryBranches.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<SubcategoryBranch>> GetAllAsync()
        {
            return await _context.SubcategoryBranches.ToListAsync();
        }
    }
}

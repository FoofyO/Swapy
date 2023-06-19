using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SwapyDbContext _context;

        public SubcategoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Subcategory item)
        {
            await _context.Subcategories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subcategory item)
        {
            _context.Subcategories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subcategory item)
        {
            _context.Subcategories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Subcategory> GetByIdAsync(string id)
        {
            var item = await _context.Subcategories.FindAsync(id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<Subcategory> GetDetailByIdAsync(string id)
        {
            var item = await _context.Subcategories.Include(s => s.ChildSubcategories)
                                                    .ThenInclude(b => b.Child)
                                                   .Include(s => s.ParentSubcategory)
                                                    .ThenInclude(b => b.Parent)
                                                   .FirstOrDefaultAsync(s => s.Id.Equals(id));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }

        /// <summary>
        /// Product Attributes
        /// </summary>
        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAnimalTypesAsync(Language language)
        {
            return _context.Subcategories.Where(s => s.Type == SubcategoryType.AnimalsType)
                                         .Include(s => s.Names)
                                         .AsEnumerable()
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                         .OrderBy(s => s.Value)
                                         .ToList();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAutoTypesAsync(Language language)
        {
            return _context.Subcategories.Where(s => s.Type == SubcategoryType.AutosType)
                                         .Include(s => s.Names)
                                         .AsEnumerable()
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                         .OrderBy(s => s.Value)
                                         .ToList();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllElectronicTypesAsync(Language language)
        {
            return _context.Subcategories.Where(s => s.Type == SubcategoryType.ElectronicsType)
                                         .Include(s => s.Names)
                                         .AsEnumerable()
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                         .OrderBy(s => s.Value)
                                         .ToList();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemSectionsAsync(Language language)
        {
            return _context.Subcategories.Include(s => s.ChildSubcategories)
                                         .Where(s => s.Type == SubcategoryType.ItemsType && s.ChildSubcategories.Count != 0)
                                         .Include(s => s.Names)
                                         .AsEnumerable()
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                         .OrderBy(s => s.Value)
                                         .ToList();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemTypesAsync(string parentSubcategoryId, Language language)
        {
            return await _context.Subcategories.Where(s => s.Type == SubcategoryType.ItemsType && s.ParentSubcategoryId.Equals(parentSubcategoryId))
                                               .Include(s => s.Names)
                                               .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                               .OrderBy(s => s.Value)
                                               .ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllRealEstateTypesAsync(Language language)
        {
            return await _context.Subcategories.Where(s => s.Type == SubcategoryType.RealEstatesType)
                                               .Include(s => s.Names)
                                               .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                               .OrderBy(s => s.Value)
                                               .ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetByCategoryAsync(string categoryId, Language language)
        {
            var item = await _context.Subcategories.Where(s => s.CategoryId.Equals(categoryId) && s.ParentSubcategoryId == null)
                                                   .Include(s => s.Names)
                                                   .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                                   .OrderBy(s => s.Value)
                                                   .ToListAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {categoryId} id not found");
            return item;
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetBySubcategoryAsync(string subcategoryId, Language language)
        {
            var item = _context.Subcategories.Where(s => s.ParentSubcategoryId.Equals(subcategoryId))
                                             .Include(s => s.Names)
                                             .AsEnumerable()
                                             .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                             .OrderBy(s => s.Value)
                                             .ToList();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {subcategoryId} id not found");
            return item;
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetClothesTypesByGenderAsync(string genderId, Language language)
        {
            return _context.Subcategories.Include(s => s.ClothesViews)
                                         .Where(s => (s.Type == SubcategoryType.ClothesType) && (s.ClothesViews.Select(cv => cv.GenderId).Contains(genderId)))
                                         .Include(s => s.Names)
                                         .AsEnumerable()
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                         .OrderBy(s => s.Value)
                                         .ToList();
        }
        
        public async Task<IEnumerable<CategoryNode>> GetSequenceOfSubcategories(string subcategoryId, Language language)
        {
            List<Subcategory> result = new();
            Subcategory currentSubcategory = await _context.Subcategories.Where(s => s.Id.Equals(subcategoryId))
                                                                         .Include(s => s.Names)
                                                                         .FirstOrDefaultAsync();

            if (currentSubcategory == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {subcategoryId} id not found");

            do
            {
                result.Insert(0, currentSubcategory);
                currentSubcategory = await _context.Subcategories.Where(s => s.ParentSubcategoryId.Equals(currentSubcategory.ParentSubcategoryId))
                                                                 .Include(s => s.Names)
                                                                 .FirstOrDefaultAsync();
            } while (currentSubcategory != null);
           
            return result.Select(s => new CategoryNode(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value)).ToList();
        }
    }
}

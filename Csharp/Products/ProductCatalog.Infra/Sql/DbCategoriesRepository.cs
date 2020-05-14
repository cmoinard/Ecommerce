using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.Ports;
using ProductCatalog.Infra.Sql.Models;
using Shared.Core;

namespace ProductCatalog.Infra.Sql
{
    public class DbCategoriesRepository : ICategoriesRepository
    {
        private readonly ProductCatalogContext _context;

        public DbCategoriesRepository(ProductCatalogContext context)
        {
            _context = context;
        }

        private DbSet<DbCategory> Set => _context.Set<DbCategory>();
        
        public async Task<Category?> GetByIdAsync(CategoryId id)
        {
            var castedId = (int) id;
            var category = await Set.FirstOrDefaultAsync(c => c.Id == castedId);

            return category?.ToDomain();
        }

        public async Task<IReadOnlyCollection<Category>> GetAsync()
        {
            var categories = await Set.ToListAsync();

            return
                categories
                    .Select(c => c.ToDomain())
                    .ToList();
        }

        public Task<bool> NameExistsAsync(CategoryName name) =>
            Set.AnyAsync(c => c.Name == (string)name);

        public async Task CreateAsync(UncreatedCategory category)
        {
            await Set.AddAsync(DbCategory.FromDomain(category));
        }

        public async Task DeleteAsync(Category category)
        {
            var castedId = (int) category.Id;
            var dbCategory = await Set.FirstOrDefaultAsync(c => c.Id == castedId);
            if (dbCategory != null)
            {
                Set.Remove(dbCategory);
            }
        }

        public async Task<IReadOnlyCollection<CategoryId>> GetNonExistentIdsAsync(NonEmptyList<CategoryId> ids)
        {
            var castedIds = ids.Select(id => (int) id).ToList();

            var nonExistentIds =
                await Set
                    .Select(c => c.Id)
                    .Where(cId => !castedIds.Contains(cId))
                    .ToListAsync();

            return
                nonExistentIds
                    .Select(cId => new CategoryId(cId))
                    .ToList();
        }
    }
}
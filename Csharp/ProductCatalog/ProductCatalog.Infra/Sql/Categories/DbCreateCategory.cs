using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Categories
{
    public class DbCreateCategory : ICreateCategory
    {
        private readonly ProductCatalogContext _context;

        public DbCreateCategory(ProductCatalogContext context)
        {
            _context = context;
        }
        
        public async Task<Category> CreateAsync(UncreatedCategory category)
        {
            var savedCategory = DbCategory.FromDomain(category);
            
            await _context.Set<DbCategory>().AddAsync(savedCategory);
            await _context.SaveChangesAsync();
            
            return savedCategory.ToDomain();
        }
    }
}
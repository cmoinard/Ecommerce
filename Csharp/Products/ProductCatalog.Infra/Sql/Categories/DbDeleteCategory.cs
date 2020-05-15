using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Categories
{
    public class DbDeleteCategory : IDeleteCategory
    {
        private readonly ProductCatalogContext _context;

        public DbDeleteCategory(ProductCatalogContext context)
        {
            _context = context;
        }
        
        public async Task DeleteAsync(Category category)
        {
            var castedId = (int) category.Id;
            var dbCategory = await _context.Set<DbCategory>().FirstOrDefaultAsync(c => c.Id == castedId);
            if (dbCategory != null)
            {
                _context.Set<DbCategory>().Remove(dbCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
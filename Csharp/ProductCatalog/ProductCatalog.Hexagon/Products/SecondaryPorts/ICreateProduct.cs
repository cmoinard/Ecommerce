using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;

namespace ProductCatalog.Hexagon.Products.SecondaryPorts
{
    public interface ICreateProduct
    {
        Task<Product> CreateAsync(UncreatedProduct product);
    }
}
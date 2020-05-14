using System.Threading.Tasks;
using ProductCatalog.Hexagon;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.Infra.InMemory
{
    public class InMemorySaveProduct : ISaveProduct
    {
        public Task SaveAsync(Product product) => Task.CompletedTask;
    }
}
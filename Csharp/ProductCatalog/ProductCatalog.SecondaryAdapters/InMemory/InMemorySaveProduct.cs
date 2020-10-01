using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.SecondaryAdapters.InMemory
{
    public class InMemorySaveProduct : ISaveProduct
    {
        public Task SaveAsync(Product product) => Task.CompletedTask;
    }
}
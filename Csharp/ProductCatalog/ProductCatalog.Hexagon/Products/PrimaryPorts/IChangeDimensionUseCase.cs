using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeDimensionUseCase
    {
        Task ChangeDimensionsAsync(ProductId productId, Dimension dimension);
    }
}
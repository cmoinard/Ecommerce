using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.AcceptanceTests.Products
{
    public abstract class TestBase
    {
        public IProductsRepository RepositoryReturning(Product product)
        {
            var repository = Substitute.For<IProductsRepository>();
            repository.GetByIdAsync(Arg.Any<ProductId>()).Returns(product);
            return repository;
        }

        public IProductsRepository RepositoryThatCantFindProduct()
        {
            var repository = Substitute.For<IProductsRepository>();
            repository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();
            return repository;
        }
    }
}
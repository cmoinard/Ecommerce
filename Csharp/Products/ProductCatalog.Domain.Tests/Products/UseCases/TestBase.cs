using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;

namespace ProductCatalog.Domain.Tests.Products.UseCases
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
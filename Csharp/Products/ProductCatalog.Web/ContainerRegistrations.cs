using ProductCatalog.Domain;
using ProductCatalog.Domain.Categories.Ports;
using ProductCatalog.Domain.Products.Ports;
using ProductCatalog.Infra.InMemory;
using ProductCatalog.Infra.Sql;
using SimpleInjector;

namespace ProductCatalog.Web
{
    public static class ContainerRegistrations
    {
        public static void Register(Container container)
        {
            // // In memory
            // container.RegisterSingleton<ICategoriesRepository, CategoriesRepository>();
            // container.RegisterSingleton<IProductsRepository, ProductsRepository>();
            // container.RegisterSingleton<IProductCatalogUnitOfWork, InMemoryProductCatalogUnitOfWork>();
            
            // Database
            container.Register<ICategoriesRepository, DbCategoriesRepository>();
            container.Register<IProductsRepository, DbProductsRepository>();
            container.Register<IProductCatalogUnitOfWork, DbProductCatalogUnitOfWork>();
        }
    }
}
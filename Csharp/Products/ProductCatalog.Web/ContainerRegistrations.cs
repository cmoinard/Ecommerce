using ProductCatalog.ApplicationServices;
using ProductCatalog.ApplicationServices.Categories;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.Infra.InMemory;
using SimpleInjector;

namespace ProductCatalog.Web
{
    public static class ContainerRegistrations
    {
        public static void Register(Container container)
        {
            container.RegisterSingleton<ICategoriesRepository, CategoriesRepository>();
            container.RegisterSingleton<IProductsRepository, ProductsRepository>();
            container.RegisterSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
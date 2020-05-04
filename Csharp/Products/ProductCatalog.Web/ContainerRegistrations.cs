using ProductCatalog.ApplicationServices;
using ProductCatalog.ApplicationServices.Categories;
using ProductCatalog.ApplicationServices.Products;
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
            // container.RegisterSingleton<IUnitOfWork, UnitOfWork>();
            
            // Database
            container.Register<ICategoriesRepository, DbCategoriesRepository>();
            container.Register<IProductsRepository, DbProductsRepository>();
            container.Register<IUnitOfWork, DbUnitOfWork>();
        }
    }
}
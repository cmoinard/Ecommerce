using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Infra.Sql;
using SimpleInjector;

namespace ProductCatalog.Web
{
    public static class ContainerRegistrations
    {
        public static void Register(Container container)
        {
            RegisterPrimaryPorts(container);
            RegisterSecondaryPorts(container);
        }

        private static void RegisterPrimaryPorts(Container container)
        {
            container.Register<ICreateCategoryUseCase, CreateCategoryUseCase>();
            container.Register<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
            container.Register<IGetCategoryUseCase, GetCategoryUseCase>();

            container.Register<ICreateProductUseCase, CreateProductUseCase>();
            container.Register<IDeleteProductUseCase, DeleteProductUseCase>();
            container.Register<IChangeCategoriesUseCase, ChangeCategoriesUseCase>();
            container.Register<IChangeDescriptionUseCase, ChangeDescriptionUseCase>();
            container.Register<IChangeDimensionUseCase, ChangeDimensionUseCase>();
            container.Register<IChangeNameUseCase, ChangeNameUseCase>();
            container.Register<IChangeWeightUseCase, ChangeWeightUseCase>();
        }
        
        private static void RegisterSecondaryPorts(Container container)
        {
            // // In memory
            // container.RegisterSingleton<ICategoriesRepository, CategoriesRepository>();
            // container.RegisterSingleton<IProductsRepository, ProductsRepository>();
            // container.RegisterSingleton<IProductCatalogUnitOfWork, InMemoryProductCatalogUnitOfWork>();

            // Database
            container.Register<ICategoriesRepository, DbCategoriesRepository>();
            container.Register<IProductsRepository, DbProductsRepository>();
            container.Register<ISaveProduct, DbSaveProduct>();
        }
    }
}
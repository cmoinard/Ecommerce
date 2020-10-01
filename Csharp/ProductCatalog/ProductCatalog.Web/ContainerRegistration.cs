using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.SecondaryAdapters;
using ProductCatalog.SecondaryAdapters.InMemory;
using Shared.Web;
using Shared.Web.Registration;

namespace ProductCatalog.Web
{
    public class ContainerRegistration : HexagonRegistration
    {
        public ContainerRegistration(IContainer container) 
            : base(container)
        {
        }

        public override void RegisterPrimaryPorts()
        {
            Container.Register<IGetCategoriesUseCase, GetCategoriesUseCase>();
            Container.Register<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
            Container.Register<ICreateCategoryUseCase, CreateCategoryUseCase>();

            Container.Register<IGetProductsUseCase, GetProductsUseCase>();
            Container.Register<ICreateProductUseCase, CreateProductUseCase>();
            Container.Register<IDeleteProductUseCase, DeleteProductUseCase>();
            Container.Register<IChangeNameUseCase, ChangeNameUseCase>();
            Container.Register<IChangeDescriptionUseCase, ChangeDescriptionUseCase>();
            Container.Register<IChangeDimensionUseCase, ChangeDimensionUseCase>();
            Container.Register<IChangeWeightUseCase, ChangeWeightUseCase>();
            Container.Register<IChangeCategoriesUseCase, ChangeCategoriesUseCase>();
            
        }

        public override void RegisterSecondaryPorts()
        {
            Container.Register<ICategoriesRepository, InMemoryCategoriesRepository>();
            Container.Register<IProductsRepository, InMemoryProductsRepository>();
            Container.Register<ISaveProduct, InMemorySaveProduct>();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Infra.Sql;
using ProductCatalog.Infra.Sql.Categories;
using ProductCatalog.Infra.Sql.Products;
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
            Container.Register<ICreateCategoryUseCase, CreateCategoryUseCase>();
            Container.Register<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
            Container.Register<IGetCategoryUseCase, GetCategoryUseCase>();

            Container.Register<ICreateProductUseCase, CreateProductUseCase>();
            Container.Register<IDeleteProductUseCase, DeleteProductUseCase>();
            Container.Register<IChangeCategoriesUseCase, ChangeCategoriesUseCase>();
            Container.Register<IChangeDescriptionUseCase, ChangeDescriptionUseCase>();
            Container.Register<IChangeDimensionUseCase, ChangeDimensionUseCase>();
            Container.Register<IChangeNameUseCase, ChangeNameUseCase>();
            Container.Register<IChangeWeightUseCase, ChangeWeightUseCase>();
        }

        public override void RegisterSecondaryPorts()
        {
            Container.Register<ICategoriesRepository, DbCategoriesRepository>(); 
            Container.Register<ICreateCategory, DbCreateCategory>();
            Container.Register<IDeleteCategory, DbDeleteCategory>();
            
            Container.Register<IProductsRepository, DbProductsRepository>();
            Container.Register<ISaveProduct, DbSaveProduct>();
            Container.Register<ICreateProduct, DbCreateProduct>();
            Container.Register<IDeleteProduct, DbDeleteProduct>();
        }

        public override void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductCatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Hexagonal")));
        }
    }
}
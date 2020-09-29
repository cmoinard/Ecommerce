using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.SecondaryAdapters;
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
        }

        public override void RegisterSecondaryPorts()
        {
            Container.Register<ICategoriesRepository, InMemoryCategoriesRepository>();
        }
    }
}
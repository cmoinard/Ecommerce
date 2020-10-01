using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
using Pricing.Hexagon.ProductDiscounts.SecondaryPorts;
using Pricing.Hexagon.ProductDiscounts.UseCases;
using Pricing.Hexagon.Products.PrimaryPorts;
using Pricing.Hexagon.Products.SecondaryPorts;
using Pricing.Hexagon.Products.UseCases;
using Pricing.SecondaryAdapters;
using Shared.Web;
using Shared.Web.Registration;

namespace Pricing.Web
{
    public class ContainerRegistration : HexagonRegistration
    {
        public ContainerRegistration(IContainer container) 
            : base(container)
        {
        }

        public override void RegisterPrimaryPorts()
        {
            Container.Register<IGetProductDiscountStrategiesUseCase, GetProductDiscountStrategiesUseCase>();
            
            Container.Register<IGetProductPricesUseCase, GetProductPricesUseCase>();
            Container.Register<ISetProductPriceUseCase, SetProductProductPriceUseCase>();
        }

        public override void RegisterSecondaryPorts()
        {
            Container.Register<IProductWeightRepository, AclProductWeightRepository>();
            Container.Register<IProductPricesRepository, InMemoryProductPricesRepository>();
        }
    }
}
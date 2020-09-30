using Shared.Web;
using Shared.Web.Registration;

namespace Application.Web
{
    public static class HexagonRegistrationFactory
    {
        public static IHexagonRegistration AllHexagonRegistration(IContainer container) =>
            new CompositeHexagonRegistration(
                new ProductCatalog.Web.ContainerRegistration(container),
                new Pricing.Web.ContainerRegistration(container));
    }
}
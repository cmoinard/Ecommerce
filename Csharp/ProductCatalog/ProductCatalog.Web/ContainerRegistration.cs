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
        }

        public override void RegisterSecondaryPorts()
        {
        }
    }
}
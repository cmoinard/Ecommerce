using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Web.Registration
{
    public abstract class HexagonRegistration : IHexagonRegistration
    {
        protected IContainer Container { get; }

        protected HexagonRegistration(IContainer container)
        {
            Container = container;
        }

        public abstract void RegisterPrimaryPorts();
        public abstract void RegisterSecondaryPorts();

        public virtual void RegisterOther()
        {
        }

        public virtual void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
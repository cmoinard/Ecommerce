using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Web.Registration
{
    public interface IHexagonRegistration
    {
        void RegisterPrimaryPorts();
        void RegisterSecondaryPorts();
        void RegisterOther();

        void RegisterDbContext(IServiceCollection services, IConfiguration configuration);
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Web.Registration
{
    public class CompositeHexagonRegistration : IHexagonRegistration
    {
        private readonly IHexagonRegistration[] _hexagonRegistrations;

        public CompositeHexagonRegistration(params IHexagonRegistration[] hexagonRegistrations)
        {
            _hexagonRegistrations = hexagonRegistrations;
        }

        public void RegisterPrimaryPorts()
        {
            foreach (var hexagonRegistration in _hexagonRegistrations) 
                hexagonRegistration.RegisterPrimaryPorts();
        }

        public void RegisterSecondaryPorts()
        {
            foreach (var hexagonRegistration in _hexagonRegistrations) 
                hexagonRegistration.RegisterSecondaryPorts();
        }

        public void RegisterOther()
        {
            foreach (var hexagonRegistration in _hexagonRegistrations) 
                hexagonRegistration.RegisterOther();
        }

        public void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            foreach (var hexagonRegistration in _hexagonRegistrations) 
                hexagonRegistration.RegisterDbContext(services, configuration);
        }
    }
}
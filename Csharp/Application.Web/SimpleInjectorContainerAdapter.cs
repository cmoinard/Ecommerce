using Shared.Web;
using SimpleInjector;

namespace Application.Web
{
    public class SimpleInjectorContainerAdapter : IContainer
    {
        private readonly Container _container;

        public SimpleInjectorContainerAdapter(Container container)
        {
            _container = container;
        }
        
        public void Register<TInterface, T>() 
            where TInterface : class
            where T : class, TInterface
        {
            _container.Register<TInterface, T>();
        }

        public void Register<T>()
            where T : class
        {
            _container.Register<T>();
        }
    }
}
namespace Shared.Web
{
    public interface IContainer
    {
        void Register<TInterface, T>() 
            where TInterface : class
            where T : class, TInterface; 
        
        void Register<T>()
            where T : class;
    }
}
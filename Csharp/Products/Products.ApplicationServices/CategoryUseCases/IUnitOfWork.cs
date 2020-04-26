using System.Threading.Tasks;

namespace Products.ApplicationServices.CategoryUseCases
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
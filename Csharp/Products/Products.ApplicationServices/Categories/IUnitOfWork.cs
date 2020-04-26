using System.Threading.Tasks;

namespace Products.ApplicationServices.Categories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class GetCategoriesUseCase : IGetCategoriesUseCase
    {
        private readonly ICategoriesRepository _repository;

        public GetCategoriesUseCase(
            ICategoriesRepository repository)
        {
            _repository = repository;
        }
        
        public Task<IReadOnlyCollection<Category>> GetAllAsync() => 
            _repository.GetAllAsync();
    }
}
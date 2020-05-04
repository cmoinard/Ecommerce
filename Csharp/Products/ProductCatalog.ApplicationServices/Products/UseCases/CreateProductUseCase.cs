using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using Shared.Core;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class CreateProductUseCase
    {
        private readonly IProductsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task CreateAsync(UnvalidatedProduct state)
        {
            state.EnsureIsValid();

            var name = state.Name.ToNonEmpty();
            var nameAlreadyExists = await _repository.NameExistsAsync(name);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(name);

            var product = state.ToDomain();

            await _repository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public class ProductNameAlreadyExistsException : DomainException
    {
        public ProductNameAlreadyExistsException(NonEmptyString name)
        {
            Name = name;
        }

        public NonEmptyString Name { get; }
    }
}
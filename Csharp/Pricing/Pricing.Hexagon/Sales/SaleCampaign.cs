using System.Collections.Generic;
using Pricing.Hexagon.Discounts;
using Shared.Core.DomainModeling;
using Shared.Core.Exceptions;
using Shared.Domain;

namespace Pricing.Hexagon.Sales
{
    public class SaleCampaign : AggregateRoot<SaleCampaignId>
    {
        private readonly Dictionary<ProductId, ProductDiscount> _discountByProductId =
            new Dictionary<ProductId, ProductDiscount>();

        public SaleCampaign(SaleCampaignId id, Period period)
            : base(id)
        {
            Period = period;
        }

        public Period Period { get; }

        public bool IsActive { get; private set; }

        public IReadOnlyCollection<ProductDiscount> ProductDiscounts =>
            _discountByProductId.Values;

        public void Activate() =>
            IsActive = true;

        public void Deactivate() =>
            IsActive = false;

        public void AddDiscount(ProductId productId, IDiscount discount)
        {
            if (_discountByProductId.ContainsKey(productId))
                throw new ProductIdAlreadyHasADiscountException(productId);

            _discountByProductId[productId] = new ProductDiscount(productId, discount);
        }

        public void RemoveDiscount(ProductId productId)
        {
            if (_discountByProductId.ContainsKey(productId))
                _discountByProductId.Remove(productId);
        }

        public class ProductIdAlreadyHasADiscountException : DomainException
        {
            public ProductIdAlreadyHasADiscountException(ProductId productId)
            {
                ProductId = productId;
            }

            public ProductId ProductId { get; }
        }
    }
}
using System;
using Shared.Core.DomainModeling;

namespace Pricing.Hexagon.Sales
{
    public class SaleCampaignId : Id<Guid>
    {
        public SaleCampaignId(Guid internalValue) 
            : base(internalValue)
        {
        }
    }
}
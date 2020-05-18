using System;

namespace ProductCatalog.Infra.Sql.Models
{
    public class DbProductCategory
    {
        public Guid ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
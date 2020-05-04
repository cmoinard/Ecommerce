using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;

namespace ProductCatalog.Web.Products.Dtos
{
    public class DimensionDto
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public UnvalidatedDimension ToDomain() => 
            new UnvalidatedDimension(Length, Width, Height);
    }
}
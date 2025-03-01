namespace ProductService.Entity
{
    public class Brands
    {
        public int Id { get; set; }

        public List<BrandsSubcategories>? BrandsSubcategories { get; set; } = new List<BrandsSubcategories>();

        public string? Name { get; set; }
    }
}

namespace ProductService.Entity
{
    public class Subcategories
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<Product>? Products { get; set; } = new List<Product>();

        public List<BrandsSubcategories>? BrandsSubcategories { get; set; } = new List<BrandsSubcategories>();

        public string Name { get; set; }
    }
}

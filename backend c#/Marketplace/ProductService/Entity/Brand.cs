namespace ProductService.Entity
{
    public class Brand
    {
        public int Id { get; set; }

        public List<Subcategories>? Subcategories { get; set; } = new List<Subcategories>();

        public string? Name { get; set; }
    }
}



namespace ProductService.Entity
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Subcategories>? Subcategories { get; set; } = new List<Subcategories>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProductService.Model.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BrandsSubcategoriesId { get; set; }

        public BrandsSubcategories? BrandsSubcategories { get; set; }

        public List<ProductSizes> ProductSizes { get; set; } = new List<ProductSizes>();

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ProductService.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int BrandsSubcategoriesId { get; set; }

        public BrandsSubcategories? BrandsSubcategories { get; set; }

        public string Name { get; set; }

        public int Price {  get; set; }

        public int Count { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}

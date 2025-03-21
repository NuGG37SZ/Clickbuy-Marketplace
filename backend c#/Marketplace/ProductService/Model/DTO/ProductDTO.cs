namespace ProductService.Model.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BrandsSubcategoriesId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}

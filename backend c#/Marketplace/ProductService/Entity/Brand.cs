namespace ProductService.Entity
{
    public class Brand
    {
        public int Id { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        public string Name { get; set; }


    }
}

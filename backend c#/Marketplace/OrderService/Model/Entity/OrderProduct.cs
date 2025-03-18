using System.ComponentModel.DataAnnotations;

namespace OrderService.Model.Entity
{
    public class OrderProduct
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductSizesId { get; set; }

        public int Count { get; set; }
    }
}

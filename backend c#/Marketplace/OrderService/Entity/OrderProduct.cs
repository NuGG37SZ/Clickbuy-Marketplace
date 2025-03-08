using System.ComponentModel.DataAnnotations;

namespace OrderService.Entity
{
    public class OrderProduct
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CartId { get; set; }

        public Order? Order { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace OrderService.Entity
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreateOrder {  get; set; }

        public DateTime? UpdateOrder { get; set; }

        [Required]
        public string Status { get; set; }
    }
}

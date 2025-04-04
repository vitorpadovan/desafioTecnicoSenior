using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities
{
    public class OrderDetail
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("order")]
        public Order Order { get; set; }

        [Column("product")]
        public Product Product { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
    }
}

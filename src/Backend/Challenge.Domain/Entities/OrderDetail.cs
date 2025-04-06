using Challenge.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Challenge.Domain.Entities
{
    public class OrderDetail
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("order")]
        [JsonIgnore]
        public Order Order { get; set; }

        [Column("product")]
        public Product Product { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("state")]
        public OrderItemState State { get; set; } = OrderItemState.Saved;
    }
}

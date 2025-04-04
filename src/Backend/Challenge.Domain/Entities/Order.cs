using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities
{
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("reseller")]
        public required Reseller Reseller { get; set; }

        [Column("clientuserid")]
        public required Guid ClientUserId { get; set; }

        [Column("total")]
        public decimal Total { get; set; }

        [Column("recivedat")]
        public DateTime RecivedAt { get; set; }

        [Column("ordersenttofactory")]
        public DateTime? OrderSentToFactory { get; set; }

        [Column("shippedat")]
        public DateTime? ShippedAt { get; set; }

        [Column("orderdetails")]
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }
}

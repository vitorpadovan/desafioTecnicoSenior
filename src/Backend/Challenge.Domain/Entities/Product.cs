using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities
{
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; } = 0;

        [Column("resellerid")]
        public Guid ResellerId { get; set; }

        [Column("stock")]
        public int Stock { get; set; } = 0;
        
        [Column("reservedstock")]
        public int ReservedStock { get; set; } = 0;

        [Column("isactive")]
        public bool IsActive { get; set; } = true;

        public required Reseller Reseller { get; set; }
    }
}

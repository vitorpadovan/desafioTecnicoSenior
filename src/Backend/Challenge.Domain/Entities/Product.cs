using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;
using Challenge.Domain.Enums;

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
        [BsonElement("stock")] // Nome do campo no MongoDB
        public int Stock { get; set; } = 0;
        
        [Column("reservedstock")]
        [BsonElement("reservedstock")] // Nome do campo no MongoDB
        public int ReservedStock { get; set; } = 0;

        public ProductState State { get; set; } = ProductState.Saved;

        public required Reseller Reseller { get; set; }
    }
}

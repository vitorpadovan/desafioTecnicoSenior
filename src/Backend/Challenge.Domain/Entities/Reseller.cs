using Challenge.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Challenge.Domain.Entities
{
    public class Reseller
    {
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("document")]
        public required string Document { get; set; }

        [Column("registredname")]
        public required string RegistredName { get; set; }

        [Column("tradename")]
        public required string TradeName { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        public List<Contact> Contacts { get; set; } = new();

        public required List<Address> Addresses { get; set; } = new();

        public EntityState State { get; set; }

        public List<ResellerUsers> ResellerUsers { get; set; } = new();

        public override string? ToString()
        {
            return JsonSerializer.Serialize(new
            {
                this.Id,
                this.Document,
                this.TradeName,
                this.RegistredName,
                this.Email,
                this.State
            });
        }
    }
}

using Challenge.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<Contact> Contacts { get; set; } = new(); // Relação com Contact

        public required List<Address> Addresses { get; set; } = new(); // Relação com Address

        public EntityState State { get; set; }
    }
}

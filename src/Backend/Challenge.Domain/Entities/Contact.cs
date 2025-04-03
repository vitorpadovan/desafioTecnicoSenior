using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities
{
    public class Contact
    {
        [Column("id")]
        public Guid Id { get; set; }
    }
}

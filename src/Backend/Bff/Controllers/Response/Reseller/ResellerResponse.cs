using System.ComponentModel.DataAnnotations.Schema;

namespace Bff.Controllers.Response.Reseller
{
    public class ResellerResponse
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("document")]
        public required string Document { get; set; }

        [Column("registredname")]
        public required string RegistredName { get; set; }

        [Column("tradename")]
        public required string TradeName { get; set; }

        [Column("email")]
        public required string Email { get; set; }
    }
}

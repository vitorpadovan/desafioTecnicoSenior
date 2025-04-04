using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities
{
    public class Address
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("postalcode")]
        public int PostalCode { get; set; }

        [Column("resellerdocumentid")]
        public string ResellerDocumentId { get; set; } // Propriedade para a chave estrangeira

        public Reseller Reseller { get; set; } // Relação com Reseller
    }
}

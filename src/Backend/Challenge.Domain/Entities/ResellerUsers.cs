using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities
{
    public class ResellerUsers
    {
        [Column("id")]
        public int Id { get; set; }
        public IdentityUser Users { get; set; }
        public List<Reseller> Reseller { get; set; } = [];
    }
}

using Challenge.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

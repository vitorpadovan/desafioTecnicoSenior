using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Orm.Mapping
{
    public class ContactDbConfiguration : BasicDbConfiguration<Contact>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(builder => builder.Id);
        }
    }
}

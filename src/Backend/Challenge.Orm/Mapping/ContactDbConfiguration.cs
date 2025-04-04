using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

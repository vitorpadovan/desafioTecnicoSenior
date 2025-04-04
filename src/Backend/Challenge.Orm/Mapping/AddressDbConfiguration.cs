using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm.Mapping
{
    public class AddressDbConfiguration : BasicDbConfiguration<Address>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(a => a.Reseller)
                   .WithMany(r => r.Addresses)
                   .HasPrincipalKey(r => r.Document)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm.Mapping
{
    public class ResellerUsersDbConfiguration : BasicDbConfiguration<ResellerUsers>
    {
        protected override void ConfigureChild(EntityTypeBuilder<ResellerUsers> builder)
        {
            builder.HasMany(x => x.Reseller)
                .WithMany(x => x.ResellerUsers)
                .UsingEntity(j =>
                {
                    j.ToTable("resselertouser");
                    j.Property("ResellerId").HasColumnName("resellerid");
                    j.Property("ResellerUsersId").HasColumnName("resellerusersid");
                });
        }
    }
}

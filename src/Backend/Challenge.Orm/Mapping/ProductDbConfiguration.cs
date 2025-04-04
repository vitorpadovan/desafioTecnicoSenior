using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm.Mapping
{
    public class ProductDbConfiguration : BasicDbConfiguration<Product>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(x => x.Name);
            builder.HasOne(x => x.Reseller)
               .WithMany(x => x.Products)
               .HasForeignKey(x => x.ResellerId);
            builder.HasIndex(x => new { x.Name, x.ResellerId }).IsUnique();
        }
    }
}

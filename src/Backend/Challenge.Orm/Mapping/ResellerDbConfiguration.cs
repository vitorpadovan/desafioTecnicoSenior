using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm.Mapping
{
    public class ResellerDbConfiguration : BasicDbConfiguration<Reseller>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Reseller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Document).IsUnique(); // Adiciona índice único para o campo Document

            builder.HasMany(r => r.Addresses) // Configura a relação com Address
                   .WithOne(a => a.Reseller) // Define a relação inversa
                   .HasForeignKey(a => a.ResellerDocumentId)
                   .OnDelete(DeleteBehavior.Cascade); // Adiciona comportamento de exclusão em cascata
        }
    }
}

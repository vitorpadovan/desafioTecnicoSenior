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
            builder.HasOne(a => a.Reseller) // Configura a relação com Reseller
                   .WithMany(r => r.Addresses)     // Define a relação inversa
                   .HasPrincipalKey(r => r.Document) // Define a chave primária do Reseller
                   .OnDelete(DeleteBehavior.Cascade); // Adiciona comportamento de exclusão em cascata
        }
    }
}

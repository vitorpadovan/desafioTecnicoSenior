using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm.Mapping
{
    class OrderDbConfiguration : BasicDbConfiguration<Order>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Order> builder)
        {

        }
    }
}

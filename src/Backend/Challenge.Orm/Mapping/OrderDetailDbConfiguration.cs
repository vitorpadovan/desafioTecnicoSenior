using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm.Mapping
{
    public class OrderDetailDbConfiguration : BasicDbConfiguration<OrderDetail>
    {
        protected override void ConfigureChild(EntityTypeBuilder<OrderDetail> builder)
        {

        }
    }
}

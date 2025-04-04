using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Orm
{
    public abstract class BasicDbConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        protected abstract void ConfigureChild(EntityTypeBuilder<T> builder);
        //protected abstract void UniqueKeys(EntityTypeBuilder<T> builder);
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name.ToLower());
            this.ConfigureChild(builder);
            //var propBase = typeof(T)
            //    .GetProperties()
            //    .Where(p => !IsList(p))
            //    .ToList();
            //propBase.ForEach(p =>
            //{
            //    builder.Property(p.Name).HasColumnName(p.Name.ToLower());
            //});
        }

        private static bool IsList(System.Reflection.PropertyInfo p)
        {
            return p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}

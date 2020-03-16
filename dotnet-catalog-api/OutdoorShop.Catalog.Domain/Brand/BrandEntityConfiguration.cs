namespace OutdoorShop.Catalog.Domain.Brand
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OutdoorShop.Catalog.Domain.Entities;

    public class BrandEntityConfiguration : IEntityTypeConfiguration<BrandEntity>
    {
        public void Configure(EntityTypeBuilder<BrandEntity> builder)
        {
            builder.ToTable(DataConstants.Tables.Brands);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(DataConstants.Columns.Id);
            builder.Property(x => x.Name).HasColumnName(DataConstants.Columns.Name);
        }
    }
}

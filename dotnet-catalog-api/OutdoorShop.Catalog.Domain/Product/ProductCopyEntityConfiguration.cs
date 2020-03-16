namespace OutdoorShop.Catalog.Domain.Product
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductCopyEntityConfiguration : IEntityTypeConfiguration<ProductCopyEntity>
    {
        public void Configure(EntityTypeBuilder<ProductCopyEntity> builder)
        {
            builder.ToTable(DataConstants.Tables.ProductCopy)
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName(DataConstants.Columns.Id);

            builder.Property(x => x.LongDescription)
                .HasColumnName(DataConstants.Columns.LongDescription);

            builder.Property(x => x.Notes)
                .HasColumnName(DataConstants.Columns.Notes);

            builder.Property(x => x.Bullets)
                .HasColumnName(DataConstants.Columns.Bullets);
        }
    }
}

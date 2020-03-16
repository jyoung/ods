namespace OutdoorShop.Catalog.Domain.Product
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable(DataConstants.Tables.Products)
                    .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName(DataConstants.Columns.Id);

            builder.Property(x => x.ItemNumber)
                .HasColumnName(DataConstants.Columns.ItemNumber);
            
            builder.Property(x => x.Title)
                .HasColumnName(DataConstants.Columns.Title);
            
            builder.Property(x => x.ShortDescription)
                .HasColumnName(DataConstants.Columns.ShortDescription);
            
            builder.Property(x => x.RetailPrice)
                .HasColumnName(DataConstants.Columns.RetailPrice);
            
            builder.Property(x => x.RetailCurrency)
                .HasColumnName(DataConstants.Columns.RetailCurrency);
            
            builder.Property(x => x.SmallImageUrl)
                .HasColumnName(DataConstants.Columns.SmallImageUrl);
            
            builder.Property(x => x.LargeImageUrl)
                .HasColumnName(DataConstants.Columns.LargeImageUrl);

            builder.HasOne(x => x.Copy)
                .WithOne(a => a.Product)
                .HasForeignKey<ProductCopyEntity>(DataConstants.Columns.ProductId);
        }
    }
}

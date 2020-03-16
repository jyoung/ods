namespace OutdoorShop.Catalog.Domain.Category
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryEntityConfigurations : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable(DataConstants.Tables.Categories)
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName(DataConstants.Columns.Id);

            builder.Property(x => x.Name)
                .HasColumnName(DataConstants.Columns.Name);

            builder.HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(DataConstants.Columns.ParentId);

        }
    }
}

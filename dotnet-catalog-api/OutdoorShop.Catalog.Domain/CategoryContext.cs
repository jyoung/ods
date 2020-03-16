namespace OutdoorShop.Catalog.Domain
{
    using Microsoft.EntityFrameworkCore;
    using OutdoorShop.Catalog.Domain.Category;
    using OutdoorShop.Catalog.Domain.Entities;
    using OutdoorShop.Catalog.Domain.Product;

    public class CategoryContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get;  set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) { }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DataConstants.Schemas.Catalog);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryContext).Assembly);
        }
    }
}

namespace OutdoorShop.Catalog.Domain
{
    using Microsoft.EntityFrameworkCore;
    using OutdoorShop.Catalog.Domain.Entities;

    public class CatalogContext : DbContext
    {
        public DbSet<BrandEntity> Brands { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DataConstants.Schemas.Catalog);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }
    }
}

namespace OutdoorShop.Catalog.Domain
{
    using Dapper.FluentMap.Configuration;
    using OutdoorShop.Catalog.Domain.Brand;
    using OutdoorShop.Catalog.Domain.Category;

    public static class EntityMaps
    {
        public static void Initialize(FluentMapConfiguration config)
        {
            config.AddMap(new BrandEntityMap());
            config.AddMap(new CategoryEntityMap());
        }
    }
}

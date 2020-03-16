namespace OutdoorShop.Catalog.Domain
{
    using Dapper.FluentMap.Configuration;
    using OutdoorShop.Catalog.Domain.Brand.Maps;
    using OutdoorShop.Catalog.Domain.Category.Maps;
    using OutdoorShop.Catalog.Domain.Product.Maps;

    public static class EntityMaps
    {
        public static void Initialize(FluentMapConfiguration config)
        {
            config.AddMap(new BrandEntityMap());
            config.AddMap(new CategoryEntityMap());
            config.AddMap(new ProductEntityMap());
            config.AddMap(new ProductCopyEntityMap());
            config.AddMap(new ProductImageEntityMap());
        }
    }
}

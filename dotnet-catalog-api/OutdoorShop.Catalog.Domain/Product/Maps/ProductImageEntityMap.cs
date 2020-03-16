namespace OutdoorShop.Catalog.Domain.Product.Maps
{
    using Dapper.FluentMap.Mapping;

    public class ProductImageEntityMap : EntityMap<ProductImageEntity>
    {
        public ProductImageEntityMap()
        {
            Map(x => x.Id).ToColumn(DataConstants.Columns.Id);
            Map(x => x.ProductId).ToColumn(DataConstants.Columns.ProductId);
            Map(x => x.LargeImageUrl).ToColumn(DataConstants.Columns.LargeImageUrl);
            Map(x => x.SmallImageUrl).ToColumn(DataConstants.Columns.SmallImageUrl);
        }
    }
}

namespace OutdoorShop.Catalog.Domain.Product
{
    using Dapper.FluentMap.Mapping;

    public class ProductEntityMap : EntityMap<ProductEntity>
    {
        public ProductEntityMap()
        {
            Map(x => x.Id).ToColumn(DataConstants.Columns.Id);
            Map(x => x.ItemNumber).ToColumn(DataConstants.Columns.ItemNumber);
            Map(x => x.Title).ToColumn(DataConstants.Columns.Title);
            Map(x => x.ShortDescription).ToColumn(DataConstants.Columns.ShortDescription);
            Map(x => x.RetailPrice).ToColumn(DataConstants.Columns.RetailPrice);
            Map(x => x.RetailCurrency).ToColumn(DataConstants.Columns.RetailCurrency);
            Map(x => x.SmallImageUrl).ToColumn(DataConstants.Columns.SmallImageUrl);
            Map(x => x.LargeImageUrl).ToColumn(DataConstants.Columns.LargeImageUrl);
        }
    }
}

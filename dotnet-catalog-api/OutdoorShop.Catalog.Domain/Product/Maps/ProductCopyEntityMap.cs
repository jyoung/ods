namespace OutdoorShop.Catalog.Domain.Product.Maps
{
    using Dapper.FluentMap.Mapping;

    public class ProductCopyEntityMap : EntityMap<ProductCopyEntity>
    {
        public ProductCopyEntityMap()
        {
            Map(x => x.Id).ToColumn(DataConstants.Columns.Id);
            Map(x => x.LongDescription).ToColumn(DataConstants.Columns.LongDescription);
            Map(x => x.Notes).ToColumn(DataConstants.Columns.Notes);
            Map(x => x.Bullets).ToColumn(DataConstants.Columns.Bullets);
        }
    }
}

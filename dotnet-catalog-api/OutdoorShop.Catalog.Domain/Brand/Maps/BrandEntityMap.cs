namespace OutdoorShop.Catalog.Domain.Brand.Maps
{
    using Dapper.FluentMap.Mapping;
    using OutdoorShop.Catalog.Domain.Entities;

    public class BrandEntityMap : EntityMap<BrandEntity>
    {
        public BrandEntityMap()
        {
            Map(x => x.Id).ToColumn(DataConstants.Columns.Id);
            Map(x => x.Name).ToColumn(DataConstants.Columns.Name);
        }
    }
}

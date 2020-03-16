namespace OutdoorShop.Catalog.Domain.Category.Maps
{
    using Dapper.FluentMap.Mapping;

    public class CategoryEntityMap : EntityMap<CategoryEntity>
    {
        public CategoryEntityMap()
        {
            Map(x => x.Id).ToColumn(DataConstants.Columns.Id);
            Map(x => x.Name).ToColumn(DataConstants.Columns.Name);
            Map(x => x.ParentId).ToColumn(DataConstants.Columns.ParentId);
        }
    }
}

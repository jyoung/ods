namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;

    [Migration(20200311122700)]
    public class _20200311122700_CreateBrandsTable : Migration
    {
        public override void Down()
        {
            Delete.Table(DataConstants.Tables.Brands)
                .InSchema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(DataConstants.Tables.Brands)
                .InSchema(DataConstants.Schemas.Catalog)
                .WithColumn(DataConstants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(DataConstants.Columns.Name).AsString()
                .WithColumn(DataConstants.Columns.ImportId).AsInt64().Nullable();

        }
    }
}

namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;

    [Migration(20200311122700)]
    public class _20200311122700_CreateBrandsTable : Migration
    {
        public override void Down()
        {
            Delete.Table(Constants.Tables.Brands)
                .InSchema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.Brands)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.Name).AsString()
                .WithColumn(Constants.Columns.ImportId).AsInt64().Nullable();

        }
    }
}

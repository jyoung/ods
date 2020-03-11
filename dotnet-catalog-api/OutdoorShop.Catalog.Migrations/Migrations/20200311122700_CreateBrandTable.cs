namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;

    [Migration(20200311122700)]
    public class _20200311122700_CreateBrandTable : Migration
    {
        public override void Down()
        {
            Delete.Table(Constants.Tables.Brand);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.Brand)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.Name).AsString();
        }
    }
}

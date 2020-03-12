namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;

    [Migration(20200312095500)]
    public class _20200312095500_CreateCategoriesTables : Migration
    {
        public override void Down()
        {
            Delete.Table(Constants.Tables.Categories)
                .InSchema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.Categories)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.Name).AsString()
                .WithColumn(Constants.Columns.ParentId).AsInt64().Indexed().Nullable()
                .WithColumn(Constants.Columns.ImportId).AsInt64().Nullable();
        }
    }
}

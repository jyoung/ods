namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;

    [Migration(20200312095500)]
    public class _20200312095500_CreateCategoriesTables : Migration
    {
        public override void Down()
        {
            Delete.Table(DataConstants.Tables.Categories)
                .InSchema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(DataConstants.Tables.Categories)
                .InSchema(DataConstants.Schemas.Catalog)
                .WithColumn(DataConstants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(DataConstants.Columns.Name).AsString()
                .WithColumn(DataConstants.Columns.ParentId).AsInt64().Indexed().Nullable()
                .WithColumn(DataConstants.Columns.ImportId).AsInt64().Nullable();
        }
    }
}

namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;

    [Migration(20200301110250)]
    public class _20200301110250_CreateCatalogSchema : Migration
    {
        public override void Down()
        {
            Delete.Schema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Schema(DataConstants.Schemas.Catalog);
        }
    }
}

namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using System;

    [Migration(20200311113200)]
    public class _20200311113200_CreateCatalogSchema : Migration
    {
        public override void Down()
        {
            Delete.Schema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Schema(Constants.Schemas.Catalog);
        }
    }
}

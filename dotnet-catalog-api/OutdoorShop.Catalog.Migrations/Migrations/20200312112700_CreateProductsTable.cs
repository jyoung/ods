namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using System;

    [Migration(20200312112700)]
    public class _20200312112700_CreateProductsTable : Migration
    {
        public override void Down()
        {
            Delete.Table(Constants.Tables.Products)
                .InSchema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.Products)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.ItemNumber).AsString(20)
                .WithColumn(Constants.Columns.Title).AsString()
                .WithColumn(Constants.Columns.ShortDescription).AsString()
                .WithColumn(Constants.Columns.RetailPrice).AsCurrency()
                .WithColumn(Constants.Columns.RetailCurrency).AsString(3)
                .WithColumn(Constants.Columns.SmallImageUrl).AsString()
                .WithColumn(Constants.Columns.LargeImageUrl).AsString()
                .WithColumn(Constants.Columns.BrandId).AsInt64().Indexed();
        }
    }
}

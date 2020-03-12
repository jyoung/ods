namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;
    using System;

    [Migration(20200312112700)]
    public class _20200312112700_CreateProductsTable : Migration
    {
        public override void Down()
        {
            Delete.Table(DataConstants.Tables.Products)
                .InSchema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(DataConstants.Tables.Products)
                .InSchema(DataConstants.Schemas.Catalog)
                .WithColumn(DataConstants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(DataConstants.Columns.ItemNumber).AsString(20)
                .WithColumn(DataConstants.Columns.Title).AsString()
                .WithColumn(DataConstants.Columns.ShortDescription).AsString()
                .WithColumn(DataConstants.Columns.RetailPrice).AsCurrency()
                .WithColumn(DataConstants.Columns.RetailCurrency).AsString(3)
                .WithColumn(DataConstants.Columns.SmallImageUrl).AsString()
                .WithColumn(DataConstants.Columns.LargeImageUrl).AsString()
                .WithColumn(DataConstants.Columns.BrandId).AsInt64().Indexed();
        }
    }
}

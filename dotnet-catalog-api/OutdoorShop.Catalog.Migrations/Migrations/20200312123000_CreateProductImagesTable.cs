namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using System;

    [Migration(20200312123000)]
    public class _20200312123000_CreateProductImagesTable : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey(Constants.ForeignKeys.ProductImagesToProducts)
                .OnTable(Constants.Tables.ProductImages)
                .InSchema(Constants.Schemas.Catalog);

            Delete.Table(Constants.Tables.ProductImages)
                .InSchema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.ProductImages)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.ProductId).AsInt64().NotNullable().Indexed()
                .WithColumn(Constants.Columns.SmallImageUrl).AsString()
                .WithColumn(Constants.Columns.LargeImageUrl).AsString();

            Create.ForeignKey(Constants.ForeignKeys.ProductImagesToProducts)
                .FromTable(Constants.Tables.ProductImages)
                    .InSchema(Constants.Schemas.Catalog)
                    .ForeignColumn(Constants.Columns.ProductId)
                .ToTable(Constants.Tables.Products)
                    .InSchema(Constants.Schemas.Catalog)
                    .PrimaryColumn(Constants.Columns.Id);
        }
    }
}

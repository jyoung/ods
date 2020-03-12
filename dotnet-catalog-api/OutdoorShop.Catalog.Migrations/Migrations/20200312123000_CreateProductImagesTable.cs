namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;
    using System;

    [Migration(20200312123000)]
    public class _20200312123000_CreateProductImagesTable : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey(DataConstants.ForeignKeys.ProductImagesToProducts)
                .OnTable(DataConstants.Tables.ProductImages)
                .InSchema(DataConstants.Schemas.Catalog);

            Delete.Table(DataConstants.Tables.ProductImages)
                .InSchema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(DataConstants.Tables.ProductImages)
                .InSchema(DataConstants.Schemas.Catalog)
                .WithColumn(DataConstants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(DataConstants.Columns.ProductId).AsInt64().NotNullable().Indexed()
                .WithColumn(DataConstants.Columns.SmallImageUrl).AsString()
                .WithColumn(DataConstants.Columns.LargeImageUrl).AsString();

            Create.ForeignKey(DataConstants.ForeignKeys.ProductImagesToProducts)
                .FromTable(DataConstants.Tables.ProductImages)
                    .InSchema(DataConstants.Schemas.Catalog)
                    .ForeignColumn(DataConstants.Columns.ProductId)
                .ToTable(DataConstants.Tables.Products)
                    .InSchema(DataConstants.Schemas.Catalog)
                    .PrimaryColumn(DataConstants.Columns.Id);
        }
    }
}

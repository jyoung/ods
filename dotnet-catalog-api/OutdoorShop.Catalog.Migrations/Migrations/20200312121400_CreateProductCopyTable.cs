namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;
    using System;

    [Migration(20200312121400)]
    public class _20200312121400_CreateProductCopyTable : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey(DataConstants.ForeignKeys.ProductCopyToProducts)
                .OnTable(DataConstants.Tables.ProductCopy)
                .InSchema(DataConstants.Schemas.Catalog);

            Delete.Table(DataConstants.Tables.ProductCopy)
                .InSchema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(DataConstants.Tables.ProductCopy)
                .InSchema(DataConstants.Schemas.Catalog)
                .WithColumn(DataConstants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(DataConstants.Columns.ProductId).AsInt64().NotNullable().Indexed()
                .WithColumn(DataConstants.Columns.LongDescription).AsString()
                .WithColumn(DataConstants.Columns.Notes).AsString()
                .WithColumn(DataConstants.Columns.Bullets).AsString();

            Create.ForeignKey(DataConstants.ForeignKeys.ProductCopyToProducts)
            .FromTable(DataConstants.Tables.ProductCopy)
                .InSchema(DataConstants.Schemas.Catalog)
                .ForeignColumn(DataConstants.Columns.ProductId)
            .ToTable(DataConstants.Tables.Products)
                .InSchema(DataConstants.Schemas.Catalog)
                .PrimaryColumn(DataConstants.Columns.Id);
        }
    }
}

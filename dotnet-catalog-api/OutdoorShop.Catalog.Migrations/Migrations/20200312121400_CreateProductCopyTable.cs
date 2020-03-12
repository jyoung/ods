namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using System;

    [Migration(20200312121400)]
    public class _20200312121400_CreateProductCopyTable : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey(Constants.ForeignKeys.ProductCopyToProducts)
                .OnTable(Constants.Tables.ProductCopy)
                .InSchema(Constants.Schemas.Catalog);

            Delete.Table(Constants.Tables.ProductCopy)
                .InSchema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.ProductCopy)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.ProductId).AsInt64().NotNullable().Indexed()
                .WithColumn(Constants.Columns.LongDescription).AsString()
                .WithColumn(Constants.Columns.Notes).AsString()
                .WithColumn(Constants.Columns.Bullets).AsString();

            Create.ForeignKey(Constants.ForeignKeys.ProductCopyToProducts)
            .FromTable(Constants.Tables.ProductCopy)
                .InSchema(Constants.Schemas.Catalog)
                .ForeignColumn(Constants.Columns.ProductId)
            .ToTable(Constants.Tables.Products)
                .InSchema(Constants.Schemas.Catalog)
                .PrimaryColumn(Constants.Columns.Id);
        }
    }
}

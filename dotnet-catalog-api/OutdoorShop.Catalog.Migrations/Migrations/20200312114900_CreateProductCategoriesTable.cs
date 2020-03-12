namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using OutdoorShop.Catalog.Domain;
    using System;

    [Migration(20200312114900)]
    public class _20200312114900_CreateProductCategoriesTable : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey(DataConstants.ForeignKeys.ProductCateoriesToProducts)
                .OnTable(DataConstants.Tables.ProductCategories)
                .InSchema(DataConstants.Schemas.Catalog);

            Delete.Table(DataConstants.Tables.ProductCategories)
                .InSchema(DataConstants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(DataConstants.Tables.ProductCategories)
                .InSchema(DataConstants.Schemas.Catalog)
                .WithColumn(DataConstants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(DataConstants.Columns.ProductId).AsInt64().NotNullable().Indexed()
                .WithColumn(DataConstants.Columns.CategoryId).AsInt64().NotNullable().Indexed();

            Create.ForeignKey(DataConstants.ForeignKeys.ProductCateoriesToProducts)
                .FromTable(DataConstants.Tables.ProductCategories)
                    .InSchema(DataConstants.Schemas.Catalog)
                    .ForeignColumn(DataConstants.Columns.ProductId)
                .ToTable(DataConstants.Tables.Products)
                    .InSchema(DataConstants.Schemas.Catalog)
                    .PrimaryColumn(DataConstants.Columns.Id);
;
        }
    }
}

namespace OutdoorShop.Catalog.Migrations.Migrations
{
    using FluentMigrator;
    using System;

    [Migration(20200312114900)]
    public class _20200312114900_CreateProductCategoriesTable : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey(Constants.ForeignKeys.ProductCateoriesToProducts)
                .OnTable(Constants.Tables.ProductCategories)
                .InSchema(Constants.Schemas.Catalog);

            Delete.Table(Constants.Tables.ProductCategories)
                .InSchema(Constants.Schemas.Catalog);
        }

        public override void Up()
        {
            Create.Table(Constants.Tables.ProductCategories)
                .InSchema(Constants.Schemas.Catalog)
                .WithColumn(Constants.Columns.Id).AsInt64().PrimaryKey().Identity()
                .WithColumn(Constants.Columns.ProductId).AsInt64().NotNullable().Indexed()
                .WithColumn(Constants.Columns.CategoryId).AsInt64().NotNullable().Indexed();

            Create.ForeignKey(Constants.ForeignKeys.ProductCateoriesToProducts)
                .FromTable(Constants.Tables.ProductCategories)
                    .InSchema(Constants.Schemas.Catalog)
                    .ForeignColumn(Constants.Columns.ProductId)
                .ToTable(Constants.Tables.Products)
                    .InSchema(Constants.Schemas.Catalog)
                    .PrimaryColumn(Constants.Columns.Id);
;
        }
    }
}

namespace OutdoorShop.Catalog.Domain
{
    using Microsoft.Extensions.DependencyInjection;
    using OutdoorShop.Catalog.Domain.Category;
    using OutdoorShop.Catalog.Domain.Product;

    public static class ServicesConfiguration
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}

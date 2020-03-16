namespace OutdoorShop.Catalog.Domain.Category
{
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> FetchAllAsync();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection db;

        public CategoryRepository(IDbConnection db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryEntity>> FetchAllAsync()
        {
            const string sql = "select * from catalog.categories";

            var categories = await db.QueryAsync<CategoryEntity>(sql);

            return categories;
        }
    }
}

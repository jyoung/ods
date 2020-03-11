namespace OutdoorShop.Catalog.Domain.Category
{
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryDocument : DocumentBase
    {
        private readonly List<CategoryDocument> subCategories = new List<CategoryDocument>();

        public string Name { get; set; }

        public IEnumerable<CategoryDocument> SubCategories { get { return this.subCategories; } }

        public CategoryDocument(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public void AddSubCategory(CategoryDocument category)
        {
            var subCategory = subCategories.SingleOrDefault(x => x.Id == category.Id);

            if ( subCategory == null)
            {
                subCategories.Add(category);
            }
        }
    }
}

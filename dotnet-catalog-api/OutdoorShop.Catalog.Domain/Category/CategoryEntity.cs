namespace OutdoorShop.Catalog.Domain.Category
{
    public class CategoryEntity : Entity
    {
        public long? ParentId { get; set; }
        
        public string Name { get; set; }
    }
}

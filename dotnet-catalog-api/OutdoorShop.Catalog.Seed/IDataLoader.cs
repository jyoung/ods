namespace OutdoorShop.Catalog.Seed
{
    using System.Threading.Tasks;

    public interface IDataLoader
    {
        Task LoadAsync();
        void Load();
    }
}
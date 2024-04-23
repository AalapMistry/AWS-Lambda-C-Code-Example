public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllItems();
    Task<Item> GetItemById(int id);
    Task<Item> CreateItem(Item item);
    Task<Item> UpdateItem(Item item);
    Task DeleteItem(int id);
}

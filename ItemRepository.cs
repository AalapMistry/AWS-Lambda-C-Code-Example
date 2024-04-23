public class ItemRepository : IItemRepository
{
    private readonly ItemDbContext _context;

    public ItemRepository(ItemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllItems()
    {
        try
        {
            return await _context.Items.ToListAsync();
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error retrieving items", ex);
        }
    }

    public async Task<Item> GetItemById(int id)
    {
        try
        {
            return await _context.Items.FindAsync(id);
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error retrieving item by ID", ex);
        }
    }

    public async Task<Item> CreateItem(Item item)
    {
        try
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error creating item", ex);
        }
    }

    public async Task<Item> UpdateItem(Item item)
    {
        try
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error updating item", ex);
        }
    }

    public async Task DeleteItem(int id)
    {
        try
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error deleting item", ex);
        }
    }
}

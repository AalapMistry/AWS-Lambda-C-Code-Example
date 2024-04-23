public class ItemRepository : IItemRepository
{
    private readonly ItemDbContext _context;

    public ItemRepository(ItemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllItems()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item> GetItemById(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item> CreateItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Item> UpdateItem(Item item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}

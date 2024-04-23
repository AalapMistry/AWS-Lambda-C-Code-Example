

public class ItemController : Controller
{
    private static List<Item> items = new List<Item>();
    private static int idCounter = 1;

    public ActionResult Index()
    {
        return View(items);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {
        item.Id = idCounter++;
        items.Add(item);
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        return View(item);
    }

    [HttpPut]
    public ActionResult Edit(Item item)
    {
        var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        return RedirectToAction("Index");
    }
     [HttpPost]
    public ActionResult Delete(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        items.Remove(item);
        return RedirectToAction("Index");
    }
}

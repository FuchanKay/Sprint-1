namespace Scripts.Game;
public class InventoryManager(ObjectFactory objectFactory)
{
    private readonly IObject[] PlayerItems =
        [
                objectFactory.CreateEmpty(),
                objectFactory.CreateEmpty(),
                objectFactory.CreateEmpty()
        ];
    public void AddItem(IObject itemObj)
    {
        for (int i = 0; i < PlayerItems.Length; i++)
        {
            if (PlayerItems[i].Id == ObjectIds.Empty)
            {
                PlayerItems[i] = itemObj.Clone();
                break;
            }
        }
    }
    public IObject GetItem(int inventoryIdx)
    {
        var item = PlayerItems[inventoryIdx].Clone();
        return item;
    }
    public void EmptySlot(int inventoryIdx)
    {
        PlayerItems[inventoryIdx] = objectFactory.CreateEmpty();
    }

    public InventoryManager Clone()
    {
        var clone = new InventoryManager(objectFactory);
        foreach (var item in PlayerItems)
        {
            clone.AddItem(item);
        }
        return clone;
    }
}
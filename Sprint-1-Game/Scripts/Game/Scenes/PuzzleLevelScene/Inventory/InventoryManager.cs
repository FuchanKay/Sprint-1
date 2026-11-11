using System;

namespace Scripts.Game;

public class InventoryManager()
{
    private readonly IObject[] PlayerItems =
        [
                ObjectFactory.CreateEmpty(),
                ObjectFactory.CreateEmpty(),
                ObjectFactory.CreateEmpty()
        ];
    public bool AddItem(IObject itemObj)
    {
        bool isAdded = false;
        for (int i = 0; i < PlayerItems.Length; i++)
        {
            if (PlayerItems[i].Id == ObjectIds.Empty)
            {
                PlayerItems[i] = itemObj.Clone();
                isAdded = true;
                break;
            }
        }
        return isAdded;
    }
    public IObject GetItem(int inventoryIdx)
    {
        var item = PlayerItems[inventoryIdx].Clone();
        return item;
    }
    public void EmptySlot(int inventoryIdx)
    {
        PlayerItems[inventoryIdx] = ObjectFactory.CreateEmpty();
    }

    public InventoryManager Clone()
    {
        var clone = new InventoryManager();
        foreach (var item in PlayerItems)
        {
            clone.AddItem(item);
        }
        return clone;
    }
    public override string ToString()
    {
        int inventorySize = PlayerItems.Length;
        string itemString = "";
        for (int i = 0; i < inventorySize; i++)
        {
            itemString += ObjectIdToString(PlayerItems[i].Id) + " ";
        }
        return itemString;
    }
    private static string ObjectIdToString(ObjectIds objectIds)
    {
        string idString = "";
        switch (objectIds)
        {
            case ObjectIds.Bomb:
                idString = "Bomb";
            break;
            case ObjectIds.Empty:
                idString = "Empty";
            break;
            case ObjectIds.Seed:
                idString = "Seed";
            break;
        }
        return idString;
    }
}
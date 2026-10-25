
using Scripts.Game;

public class LevelStateHistory
{
    public GridPointer GridPointer {get; set;}
    public InventoryManager InventoryManager {get; set;}
    public LevelStateHistory (GridPointer gridPointer, InventoryManager inventoryManager)
    {
        GridPointer = gridPointer;
        InventoryManager = inventoryManager;
    }
    public LevelStateHistory Clone ()
    {
        var clone = new LevelStateHistory(GridPointer, InventoryManager)
        {
            GridPointer = GridPointer.Clone(),
            InventoryManager = InventoryManager.Clone()
        };
        return clone;
    }
    
}
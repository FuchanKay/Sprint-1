
using Scripts.Game;

public class LevelStateHistory
{
    public GridPointer GridPointer {get; set;}
    public InventoryManager InventoryManager {get; set;}
    public LevelStateHistory (GridPointer gridPointer, InventoryManager inventoryManager)
    {
        GridPointer = gridPointer.Clone();
        InventoryManager = inventoryManager.Clone();
    }
    
}
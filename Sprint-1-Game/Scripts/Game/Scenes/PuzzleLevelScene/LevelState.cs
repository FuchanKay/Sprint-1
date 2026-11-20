
using Scripts.Game;

public class LevelState
{
    public GridPointer GridPointer { get; set; }
    public InventoryManager InventoryManager { get; set; }
    public LevelContext Context { get; set; }
    public LevelState(GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        GridPointer = gridPointer;
        InventoryManager = inventoryManager;
        Context = context;
    }
    public LevelState Clone()
    {
        var clone = new LevelState(GridPointer, InventoryManager, Context)
        {
            GridPointer = GridPointer.Clone(),
            InventoryManager = InventoryManager.Clone(),
            Context = Context.Clone()
        };
        return clone;
    }

    public bool Equals(LevelState levelState)
    {
        return GridPointer.Equals(levelState.GridPointer) && InventoryManager.Equals(levelState.InventoryManager);
    }
}
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent(Inventory inventory, InventoryManager inventoryManager, ObjectSpriteFactory objectSpriteFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        SubEventStack.Push(new UseItemSubEvent(inventory, inventoryManager, objectSpriteFactory));
    }
}
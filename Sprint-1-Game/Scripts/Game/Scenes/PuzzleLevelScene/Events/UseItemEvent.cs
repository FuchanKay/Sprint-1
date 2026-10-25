using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent(Inventory inventory, ObjectSpriteFactory objectSpriteFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        SubEventStack.Push(new UseItemSubEvent(inventory, objectSpriteFactory));
    }
}
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent(int inventory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, List<LevelStateHistory> levelStateHistory)
    {
        SubEventStack.Push(new UseItemSubEvent(inventory));
    }
}
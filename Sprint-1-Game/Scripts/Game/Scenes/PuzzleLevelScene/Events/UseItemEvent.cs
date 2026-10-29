using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class UseItemEvent(int inventory, ObjectSpriteFactory objectSpriteFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager, List<LevelStateHistory> levelStateHistory)
    {
        SubEventStack.Push(new UseItemSubEvent(inventory, objectSpriteFactory));
    }
}
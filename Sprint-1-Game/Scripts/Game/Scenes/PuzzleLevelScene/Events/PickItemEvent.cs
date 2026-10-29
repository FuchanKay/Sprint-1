using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class PickItemEvent(ObjectFactory objectFactory, TileFactory tileFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager, List<LevelStateHistory> levelStateHistory)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory, tileFactory, levelStateHistory));
        SubEventStack.Push(new PickItemSubEvent(objectFactory, SubEventStack));
    }
}
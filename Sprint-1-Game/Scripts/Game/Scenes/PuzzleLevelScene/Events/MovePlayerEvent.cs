using System.Collections.Generic;

namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, bool lockMove, ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, LevelStateHistory levelStateHistory)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory, levelStateHistory));
        if (!lockMove) SubEventStack.Push(new DirectionalInputSubEvent(direction, objectFactory, SubEventStack));
        SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
    }
}
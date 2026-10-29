using System.Collections.Generic;

namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, bool lockMove) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, List<LevelStateHistory> levelStateHistory)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, levelStateHistory));
        if (!lockMove) SubEventStack.Push(new DirectionalInputSubEvent(direction, SubEventStack));
        SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
    }
}
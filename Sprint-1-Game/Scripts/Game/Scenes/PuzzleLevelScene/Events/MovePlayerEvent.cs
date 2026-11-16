using System.Collections.Generic;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, bool lockMove) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager, List<LevelStateHistory> levelStateHistory)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, levelStateHistory));
        if (!lockMove) SubEventStack.Push(new DirectionalInputSubEvent(direction));
        SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
    }
}
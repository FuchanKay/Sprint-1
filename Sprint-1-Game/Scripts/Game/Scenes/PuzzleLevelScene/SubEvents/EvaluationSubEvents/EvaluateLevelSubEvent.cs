using System.Collections.Generic;
using System;
using Scripts.GameComponents;
namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, List<LevelStateHistory> levelStateHistory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        subEventStack.Push(new CheckNonWalkableTilesSubEvent(subEventStack, objectFactory, tileFactory));
        subEventStack.Push(new CheckLaserEntrySubEvent(objectFactory, subEventStack));
        subEventStack.Push(new EnemiesAttackSubEvent(objectFactory, subEventStack, levelStateHistory));
    }
}
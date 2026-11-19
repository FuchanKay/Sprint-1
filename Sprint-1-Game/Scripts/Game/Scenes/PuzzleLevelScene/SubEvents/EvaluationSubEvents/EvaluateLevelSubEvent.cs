using System.Collections.Generic;
using System;
using Scripts.GameComponents;
namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, List<LevelStateHistory> levelStateHistory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        subEventStack.Push(new CheckNonWalkableTilesSubEvent(subEventStack));
        subEventStack.Push(new CheckEnemiesAttackSubEvent(subEventStack, levelStateHistory));
        subEventStack.Push(new IceSlideSubEvent(subEventStack));

        bool keysExist = gridPointer.GetObjectPointers(ObjectIds.Key).Count > 0;
        bool enemiesExist = gridPointer.EnemyCollection.Count > 0;
        if(!keysExist && !enemiesExist) subEventStack.Push(new OpenExitSubEvent());
    }
}
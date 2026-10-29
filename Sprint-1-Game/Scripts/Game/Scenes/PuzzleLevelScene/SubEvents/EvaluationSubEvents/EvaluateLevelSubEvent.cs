using System.Collections.Generic;
using System;
namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory, TileFactory tileFactory,  List<LevelStateHistory> levelStateHistory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        subEventStack.Push(new CheckNonWalkableTilesSubEvent(subEventStack, tileFactory));
        subEventStack.Push(new RemoveLasersSubEvent(levelStateHistory));
        subEventStack.Push(new CheckLaserEntrySubEvent(objectFactory, subEventStack));
        subEventStack.Push(new EnemiesAttackSubEvent(subEventStack, levelStateHistory));
        subEventStack.Push(new RemoveLasersSubEvent(levelStateHistory));
        Console.WriteLine($"There are {gridPointer.EnemyCollection.Count} enemies on screen");
    }
}
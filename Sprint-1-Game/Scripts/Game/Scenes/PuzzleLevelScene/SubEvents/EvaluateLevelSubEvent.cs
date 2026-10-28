using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory, TileFactory tileFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        subEventStack.Push(new CheckNonWalkableTilesSubEvent(subEventStack, objectFactory, tileFactory));
        subEventStack.Push(new EnemiesAttackSubEvent(objectFactory, subEventStack));
        Console.WriteLine($"There are {gridPointer.EnemyCollection.Count} enemies on screen");
    }
}
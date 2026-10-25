using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        subEventStack.Push(new CheckLavaSubEvent(subEventStack, objectFactory));
        subEventStack.Push(new EnemiesAttackSubEvent(objectFactory, subEventStack));
        subEventStack.Push(new EnemiesMoveSubEvent(objectFactory));
        Console.WriteLine($"There are {gridPointer.EnemyCollection.Count} enemies on screen");
    }
}
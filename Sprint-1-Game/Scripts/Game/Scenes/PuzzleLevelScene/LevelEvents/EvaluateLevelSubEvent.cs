using System;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(SubEventCallStack subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Console.WriteLine("Evaluated!");
    }
}
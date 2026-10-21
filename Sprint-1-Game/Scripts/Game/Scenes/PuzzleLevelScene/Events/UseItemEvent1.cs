using System;
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent1 : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Console.WriteLine($"use item 1");

    }
}
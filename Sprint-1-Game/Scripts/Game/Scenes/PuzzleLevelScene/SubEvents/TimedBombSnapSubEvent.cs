namespace Scripts.Game;

using System;
using System.Collections.Generic;

public class TimedBombSnapSubEvent(List<Coordinate> timedBombCoords, Stack<ILevelSubEvent> subEventStack)
    : BombSnapSubEvent(timedBombCoords, subEventStack)
{
    public override void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        foreach (var timedBombCoord in BombCoords)
        {
            TimedBombObject timedBomb = gridPointer.GetObject(timedBombCoord) as TimedBombObject;
            if (timedBomb.TimeRemaining == 1)
            {
                ExplodeBomb(timedBombCoord, spriteGrid, gridPointer, context);
            }
            timedBomb.TimeRemaining--;
            Console.WriteLine($"Timed bomb at {timedBombCoord} has {timedBomb.TimeRemaining} time remaining");
        }
    }
}
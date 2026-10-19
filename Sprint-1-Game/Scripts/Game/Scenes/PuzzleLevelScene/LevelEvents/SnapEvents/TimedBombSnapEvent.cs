using System;
using System.Collections.Generic;
using Scripts.Game;

public class TimedBombSnapSubEvent(List<Coordinate> timedBombCoords, ObjectFactory objectFactory, SubEventCallStack subEventStack)
    : BombSnapSubEvent(timedBombCoords, objectFactory, subEventStack)
{
    public override void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
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
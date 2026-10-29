namespace Scripts.Game;
using Scripts.GameComponents;

using System;
using System.Collections.Generic;

public class TimedBombSnapSubEvent(List<Coordinate> timedBombCoords, ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack)
    : BombSnapSubEvent(timedBombCoords, objectFactory, subEventStack)
{
    public override void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        foreach (var timedBombCoord in BombCoords)
        {
            TimedBombObject timedBomb = gridPointer.GetObject(timedBombCoord) as TimedBombObject;
            if (timedBomb.TimeRemaining == 1)
            {
                ExplodeBomb(timedBombCoord, spriteGrid, gridPointer, context, audioManager);
            }
            timedBomb.TimeRemaining--;
            Console.WriteLine($"Timed bomb at {timedBombCoord} has {timedBomb.TimeRemaining} time remaining");
        }
    }
}
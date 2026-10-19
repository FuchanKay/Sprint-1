using System;
using System.Linq;
namespace Scripts.Game;

public class TimedBombSnapEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        // var timedBombPointers = gridPointer.GetObjectPointers(ObjectIds.TimedBomb);
        // var timedBombCoords = timedBombPointers.Keys.ToList();
        // bool anyBombsExploded = false;
        // foreach (var timedBombCoord in timedBombCoords)
        // {
        //     var timedBombObject = gridPointer.GetObject(timedBombCoord) as TimedBombObject;
        //     if (timedBombObject.TimeRemaining == 1)
        //     {
        //         gridPointer.SetObject(timedBombCoord, objectFactory.CreateExplosion());
        //         anyBombsExploded = true;
        //     }
        //     timedBombObject.TimeRemaining--;
        //     Console.WriteLine($"Timed bomb at {timedBombCoord} has {timedBombObject.TimeRemaining} time remaining.");
        // }
        // if (anyBombsExploded)
        // {
        //     new ExplodeObjectEvent().Execute(spriteGrid, gridPointer, context);
        // }
    }
}
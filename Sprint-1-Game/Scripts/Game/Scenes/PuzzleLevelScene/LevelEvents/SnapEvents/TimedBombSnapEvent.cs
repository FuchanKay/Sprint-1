using System;
using System.Linq;
using Scripts.Game;

public class TimedBombSnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var timedBombPointers = gridPointer.GetObjectPointers(ObjectIds.TimedBomb);
        var timedBombCoords = timedBombPointers.Keys.ToList();
        bool anyBombsExploded = false;
        foreach (var timedBombCoord in timedBombCoords)
        {
            var timedBombObject = gridPointer.GetObject(timedBombCoord) as TimedBombObject;
            if (timedBombObject.timeRemaining == 1)
            {
                gridPointer.SetObject(timedBombCoord, new ExplosionObject());
                anyBombsExploded = true;
            }
            timedBombObject.timeRemaining--;
            Console.WriteLine($"Timed bomb at {timedBombCoord} has {timedBombObject.timeRemaining} time remaining.");
        }
        if(timedBombCoords.Count > 0)
        {
            context.ShouldUpdate = true;
        }
        if(anyBombsExploded)
        {
            new ExplodeObjectEvent().Execute(spriteGrid, gridPointer, context);
        }
        
    }
}
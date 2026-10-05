using Scripts.Game;

public class TimedBombSnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        // Only executes snap behavior for bomb objects
        var timedBombPointers = gridPointer.GetObjectPointers(ObjectIds.Bomb);
        foreach (var timedBombPointer in timedBombPointers)
        {
            var timedBombCoord = timedBombPointer.Key;
            var timedBombObject = timedBombPointer.Value as TimedBombObject;
            if (timedBombObject.timeRemaining == 1)
            {
                gridPointer.SetObject(timedBombCoord, new ExplosionObject());
            }
            timedBombObject.timeRemaining--;
        }
        new ExplodeObjectEvent().Execute(spriteGrid, gridPointer, context);
    }
}
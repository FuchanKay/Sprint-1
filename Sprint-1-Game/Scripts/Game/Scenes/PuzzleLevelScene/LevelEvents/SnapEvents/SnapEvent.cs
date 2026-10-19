using System;
using System.Linq;
using Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        // playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        // spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        // spriteGrid.StartObjectAnimation(playerSprite);

        var warlockSwapEvent = new WarlockSwapEvent();
        warlockSwapEvent.Execute(spriteGrid, gridPointer, context);

        var rotationSubEvent = new BugRotateSubEvent();
        rotationSubEvent.Execute(spriteGrid, gridPointer, context);

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();
        foreach (var bombCoord in bombCoords)
        {
            Console.WriteLine($"Exploding bomb at {bombCoord}");
            ExplodeBomb(bombCoord, spriteGrid, gridPointer, context);
        }

        foreach (var timedBombCoord in timedBombCoords)
        {
            TimedBombObject timedBomb = gridPointer.GetObject(timedBombCoord) as TimedBombObject;
            if (timedBomb.TimeRemaining == 1)
            {
                Console.WriteLine($"Exploding timed bomb at {timedBombCoord}");
                ExplodeBomb(timedBombCoord, spriteGrid, gridPointer, context);
            }
            timedBomb.TimeRemaining--;
            Console.WriteLine($"Timed bomb at {timedBombCoord} has {timedBomb.TimeRemaining} time remaining");
        }
    }

    private void ExplodeBomb(Coordinate bombCoord, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        context.IsIdle = false;
       
        gridPointer.SetObject(bombCoord, objectFactory.CreateEmpty());
        spriteGrid.RemoveObjectSprite(bombCoord);
        spriteGrid.PlayEffect(EffectIds.Explosion, bombCoord);

        const int explosionRadius = 1;
        for (int x = -explosionRadius; x <= explosionRadius; x++)
        {
            for (int y = -explosionRadius; y <= explosionRadius; y++)
            {
                var targetCoord = new Coordinate(bombCoord.X + x, bombCoord.Y + y);
                if (gridPointer.GetObject(targetCoord).IsDestructible)
                {
                    gridPointer.SetObject(targetCoord, objectFactory.CreateEmpty());
                    spriteGrid.RemoveObjectSprite(targetCoord);
                }
            }
        }
    }
}
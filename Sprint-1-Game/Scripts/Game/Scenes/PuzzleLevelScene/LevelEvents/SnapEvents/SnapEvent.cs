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

        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerSprite);

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
            if (timedBomb.timeRemaining == 1)
            {
                Console.WriteLine($"Exploding timed bomb at {timedBombCoord}");
                ExplodeBomb(timedBombCoord, spriteGrid, gridPointer, context);
            }
            timedBomb.timeRemaining--;
            Console.WriteLine($"Timed bomb at {timedBombCoord} has {timedBomb.timeRemaining} time remaining");
        }

        context.IsIdle = false;
    }

    private void ExplodeBomb(Coordinate bombCoord, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        gridPointer.SetObject(bombCoord, objectFactory.CreateExplosion());
        spriteGrid.SyncObjectSprite(bombCoord, gridPointer);
        var explosionSprite = spriteGrid.GetObjectSprite(bombCoord);
        spriteGrid.StartObjectAnimation(explosionSprite);

        const int explosionRadius = 1;
        for (int x = -explosionRadius; x <= explosionRadius; x++)
        {
            for (int y = -explosionRadius; y <= explosionRadius; y++)
            {
                var targetCoord = new Coordinate(bombCoord.X + x, bombCoord.Y + y);
                if (gridPointer.GetObject(targetCoord).isDestructible)
                {
                    gridPointer.SetObject(targetCoord, objectFactory.CreateEmpty());
                    spriteGrid.RemoveObjectSprite(targetCoord);
                }
            }
        }

        gridPointer.SetObject(bombCoord, objectFactory.CreateEmpty());

        context.ShouldUpdate = true;
    }
}
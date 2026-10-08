using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class SnapEvent : ILevelEvent
{
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
            ExplodeBomb(bombCoord,spriteGrid, gridPointer, context);
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

    private void ExplodeBomb(Vector2 bombCoord, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        gridPointer.SetObject(bombCoord, new ExplosionObject());
        spriteGrid.CycleObjectSprite(bombCoord, gridPointer);
        var explosionSprite = spriteGrid.GetObjectSprite(bombCoord);
        spriteGrid.SetObjectSprite(bombCoord, bombCoord, explosionSprite);
        spriteGrid.StartObjectAnimation(explosionSprite);

        const int explosionRadius = 1;
        for (int x = -explosionRadius; x <= explosionRadius; x++)
        {
            for (int y = -explosionRadius; y <= explosionRadius; y++)
            {
                var targetCoord = new Vector2(bombCoord.X + x, bombCoord.Y + y);
                if (gridPointer.GetObject(targetCoord).isDestructible)
                {
                    gridPointer.SetObject(targetCoord, new EmptyObject());
                    spriteGrid.RemoveObjectSprite(targetCoord);
                }
            }
        }

        gridPointer.SetObject(bombCoord, new EmptyObject());

        context.ShouldUpdate = true;
    }
}
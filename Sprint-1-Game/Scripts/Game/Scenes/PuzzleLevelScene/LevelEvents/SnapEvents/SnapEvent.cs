using System;
using System.Linq;
using Scripts.Game;
using Scripts.GameComponents;

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
        var pumpkinCoords = gridPointer.GetObjectPointers(ObjectIds.Pumpkin).Keys.ToList();
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

        foreach (var pumpkinCoord in pumpkinCoords)
        {
            var pumpkin = gridPointer.GetObject(pumpkinCoord) as PumpkinObject;
            if(!pumpkin.isGrown) GrowPumpkin(pumpkinCoord, spriteGrid, gridPointer, context);
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
                if (gridPointer.GetObject(targetCoord).IsDestructible)
                {
                    gridPointer.SetObject(targetCoord, objectFactory.CreateEmpty());
                    spriteGrid.RemoveObjectSprite(targetCoord);
                }
            }
        }

        gridPointer.SetObject(bombCoord, objectFactory.CreateEmpty());

    }

    private void GrowPumpkin(Coordinate pumpkinCoord, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var pumpkinSprite = spriteGrid.GetObjectSprite(pumpkinCoord);
        pumpkinSprite = pumpkinSprite.ConvertToAnimated(AnimationNames.PumpkinGrowth);
        spriteGrid.SetObjectSprite(pumpkinCoord, pumpkinCoord, pumpkinSprite);
        spriteGrid.StartObjectAnimation(pumpkinSprite);
        pumpkinSprite.IdleName = RegionNames.GrownPumpkin;
    }
}
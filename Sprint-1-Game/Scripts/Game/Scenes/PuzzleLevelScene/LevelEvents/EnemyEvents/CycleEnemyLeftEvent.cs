using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class CycleEnemyLeftEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Vector2 EnemyCoord = new Vector2(7, 5);
        var currentEnemy = gridPointer.GetObject(EnemyCoord);

        switch (currentEnemy.Id)
        {
            case ObjectIds.BlueLizard:
                gridPointer.SetObject(EnemyCoord, new RedLizardObject());
                break;
            case ObjectIds.Skeleton:
                gridPointer.SetObject(EnemyCoord, new BlueLizardObject());
                break;
            case ObjectIds.Warlock:
                gridPointer.SetObject(EnemyCoord, new SkeletonObject());
                break;
            case ObjectIds.RedLizard:
                gridPointer.SetObject(EnemyCoord, new WarlockObject());
                break;
            default:
                gridPointer.SetObject(EnemyCoord, new BlueLizardObject());
                break;
        }
        spriteGrid.CycleObjectSprite(EnemyCoord, gridPointer);
        var newObject = gridPointer.GetObject(EnemyCoord);
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled object to {gridPointer.GetObject(EnemyCoord).Id}");
    }
}
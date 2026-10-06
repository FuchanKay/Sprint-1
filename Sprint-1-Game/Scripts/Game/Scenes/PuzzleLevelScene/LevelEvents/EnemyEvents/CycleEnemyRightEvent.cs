using Microsoft.Xna.Framework;
using Scripts.Game;

public class CycleEnemyRightEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Vector2 EnemyCoord = new Vector2(7, 5);
        var currentEnemy = gridPointer.GetObject(EnemyCoord);

        switch (currentEnemy.Id)
        {
            case ObjectIds.RedLizard:
                gridPointer.SetObject(EnemyCoord, new BlueLizardObject());
                break;
            case ObjectIds.BlueLizard:
                gridPointer.SetObject(EnemyCoord, new SkeletonObject());
                break;
            case ObjectIds.Skeleton:
                gridPointer.SetObject(EnemyCoord, new WarlockObject());
                break;
            case ObjectIds.Warlock:
                gridPointer.SetObject(EnemyCoord, new BlueLizardObject());
                break;
            default:
                gridPointer.SetObject(EnemyCoord, new BlueLizardObject());
                break;
        }
        context.ShouldUpdate = true;
    }
}
using System;
using Scripts.Game;

public class CycleObjectLeftEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var objDemoCoord = new Coordinate(7, 2);
        var currentObject = gridPointer.GetObject(objDemoCoord);

        switch (currentObject.Id)
        {
            case ObjectIds.Bomb:
                gridPointer.SetObject(objDemoCoord, new TimedBombObject());
                break;
            case ObjectIds.TimedBomb:
                gridPointer.SetObject(objDemoCoord, new ExplosionObject());
                break;
            case ObjectIds.Explosion:
                gridPointer.SetObject(objDemoCoord, new RockObject());
                break;
            case ObjectIds.Rock:
                gridPointer.SetObject(objDemoCoord, new WallObject());
                break;
            case ObjectIds.Wall:
                gridPointer.SetObject(objDemoCoord, new BombObject());
                break;
            default:
                gridPointer.SetObject(objDemoCoord, new BombObject());
                break;
        }
        spriteGrid.CycleObjectSprite(objDemoCoord, gridPointer);
        var newObject = gridPointer.GetObject(objDemoCoord);
        if(newObject.Id == ObjectIds.Explosion)
        {
            var explosionSprite = spriteGrid.GetObjectSprite(objDemoCoord.ToVector2());
            explosionSprite = explosionSprite.ConvertToAnimated(AnimationNames.Explosion);
            spriteGrid.SetObjectSprite(objDemoCoord.ToVector2(), objDemoCoord.ToVector2(), explosionSprite);
            spriteGrid.StartObjectAnimation(explosionSprite);
            
        }
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled object to {gridPointer.GetObject(objDemoCoord).Id}");
    }
}
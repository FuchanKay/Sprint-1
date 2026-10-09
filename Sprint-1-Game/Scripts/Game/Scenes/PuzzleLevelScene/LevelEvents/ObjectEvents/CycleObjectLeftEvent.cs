using System;
using Scripts.Game;

public class CycleObjectLeftEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var objDemoCoord = new Coordinate(7, 2);
        var currentObject = gridPointer.GetObject(objDemoCoord);

        switch (currentObject.Id)
        {
            case ObjectIds.Bomb:
                gridPointer.SetObject(objDemoCoord, objectFactory.CreateTimedBomb());
                break;
            case ObjectIds.TimedBomb:
                gridPointer.SetObject(objDemoCoord, objectFactory.CreateExplosion());
                break;
            case ObjectIds.Explosion:
                gridPointer.SetObject(objDemoCoord, objectFactory.CreateRock());
                break;
            case ObjectIds.Rock:
                gridPointer.SetObject(objDemoCoord, objectFactory.CreateWall());
                break;
            case ObjectIds.Wall:
                gridPointer.SetObject(objDemoCoord, objectFactory.CreateBomb());
                break;
            default:
                gridPointer.SetObject(objDemoCoord, objectFactory.CreateBomb());
                break;
        }
        spriteGrid.CycleObjectSprite(objDemoCoord, gridPointer);
        var newObject = gridPointer.GetObject(objDemoCoord);
        if (newObject.Id == ObjectIds.Explosion)
        {
            var explosionSprite = spriteGrid.GetObjectSprite(objDemoCoord);
            // explosionSprite = explosionSprite.ConvertToAnimated(AnimationNames.Explosion);
            spriteGrid.SetObjectSprite(objDemoCoord, objDemoCoord, explosionSprite);
            spriteGrid.StartObjectAnimation(explosionSprite);

        }
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled object to {gridPointer.GetObject(objDemoCoord).Id}");
    }
}
using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class CycleObjectRightEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Vector2 objDemoCoord = new Vector2(7, 2);
        var currentObject = gridPointer.GetObject(objDemoCoord);

        switch (currentObject.Id)
        {
            case ObjectIds.Bomb:
                gridPointer.SetObject(objDemoCoord, new WallObject());
                break;
            case ObjectIds.TimedBomb:
                gridPointer.SetObject(objDemoCoord, new BombObject());
                break;
            case ObjectIds.Explosion:
                gridPointer.SetObject(objDemoCoord, new TimedBombObject());
                break;
            case ObjectIds.Rock:
                gridPointer.SetObject(objDemoCoord, new ExplosionObject());
                break;
            case ObjectIds.Wall:
                gridPointer.SetObject(objDemoCoord, new RockObject());
                break;
            default:
                gridPointer.SetObject(objDemoCoord, new BombObject());
                break;
        }
        spriteGrid.CycleObjectSprite(objDemoCoord, gridPointer);
        var newObject = gridPointer.GetObject(objDemoCoord);
        if(newObject.Id == ObjectIds.Explosion)
        {
            var explosionSprite = spriteGrid.GetObjectSprite(objDemoCoord);
            explosionSprite = explosionSprite.ConvertToAnimated(AnimationNames.Explosion);
            spriteGrid.SetObjectSprite(objDemoCoord, objDemoCoord, explosionSprite);
            spriteGrid.StartObjectAnimation(explosionSprite);
            
        }
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled object at {objDemoCoord} to {gridPointer.GetObject(objDemoCoord).Id}");
        Console.WriteLine($"Object sprite at {objDemoCoord} is now {spriteGrid.GetObjectSprite(objDemoCoord).Texture.Name}");
        Console.WriteLine($"Sprite type at {objDemoCoord} is now {spriteGrid.GetObjectSprite(objDemoCoord).GetType().Name}");
    }
}
using Microsoft.Xna.Framework;
using Scripts.Game;

public class CycleObjectLeftEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Vector2 objDemoCoord = new Vector2(7, 2);
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
        context.ShouldUpdate = true;
    }
}
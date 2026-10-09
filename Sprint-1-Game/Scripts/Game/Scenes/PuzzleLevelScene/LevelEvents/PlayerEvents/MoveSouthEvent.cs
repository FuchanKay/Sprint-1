using Microsoft.Xna.Framework;
using Scripts.Game;
using Scripts.GameComponents;

public class MoveSouthEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;

        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        var isFacingSouth = playerObj.Direction == Directions.South;
        if (isFacingSouth)
        {
            MoveSouth(spriteGrid, gridPointer, context, playerCoord, playerObj, playerSprite);
        }
        else
        {
            FaceSouth(spriteGrid, context, playerCoord, playerObj, playerSprite);
        }
    }

    private static void MoveSouth(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        var south = Coordinate.South;
        var playerSouth = playerCoord + south;
        var southGrid = gridPointer.GetGrid(playerSouth);

        if (southGrid.Object.Id == ObjectIds.Empty)
        {
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkSouth);
            spriteGrid.SetObjectSprite(playerCoord, playerSouth, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(playerSouth, playerObj);
        }
        context.IsIdle = false;
    }

    private static void FaceSouth(SpriteGrid spriteGrid, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = Directions.South;

        playerSprite = playerSprite.ConvertToStatic(RegionNames.PlayerIdleSouth);
        playerSprite.IdleName = RegionNames.PlayerIdleSouth;
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);

        context.IsIdle = true;
    }
}
using Microsoft.Xna.Framework;
using Scripts.Game;
using Scripts.GameComponents;

public class MoveNorthEvent : ILevelEvent
{
    // TODO: address duplicate code between move events
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;

        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        var isFacingNorth = playerObj.Direction == Directions.North;
        if (isFacingNorth)
        {
            MoveNorth(spriteGrid, gridPointer, context, playerCoord, playerObj, playerSprite);
        }
        else
        {
            FaceNorth(spriteGrid, context, playerCoord, playerObj, playerSprite);
        }
        context.ShouldUpdate = true;
    }

    private static void MoveNorth(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Vector2 playerCoord, IObject playerObj, ISprite playerSprite)
    {
        var northCoord = new Vector2(playerCoord.X, playerCoord.Y - 1);
        var northGrid = gridPointer.GetGrid(northCoord);

        if (northGrid.Object.Id == ObjectIds.Empty)
        {
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkNorth);
            spriteGrid.SetObjectSprite(playerCoord, northCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(northCoord, playerObj);
        }
        context.IsIdle = false;
    }

    private static void FaceNorth(SpriteGrid spriteGrid, LevelContext context, Vector2 playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = Directions.North;

        playerSprite = playerSprite.ConvertToStatic(RegionNames.PlayerIdleNorth);
        playerSprite.IdleName = RegionNames.PlayerIdleNorth;
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        
        context.IsIdle = true;
    }
}
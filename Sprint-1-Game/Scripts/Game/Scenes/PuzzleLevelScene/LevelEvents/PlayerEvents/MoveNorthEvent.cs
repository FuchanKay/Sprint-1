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

    private static void MoveNorth(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        var north = new Coordinate(0, -1);
        var playerNorth = playerCoord + north;
        var northGrid = gridPointer.GetGrid(playerNorth);

        if (northGrid.Object.Id == ObjectIds.Empty)
        {
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkNorth);
            spriteGrid.SetObjectSprite(playerCoord, playerNorth, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(playerNorth, playerObj);
        }
        context.IsIdle = false;
    }

    private static void FaceNorth(SpriteGrid spriteGrid, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = Directions.North;

        playerSprite = playerSprite.ConvertToStatic(RegionNames.PlayerIdleNorth);
        playerSprite.IdleName = RegionNames.PlayerIdleNorth;
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        
        context.IsIdle = true;
    }
}
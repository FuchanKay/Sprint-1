using Scripts.GameComponents;
namespace Scripts.Game;
public class MoveEventLogic
{
    public static void Move(Directions direction, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, ObjectFactory objectFactory)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        var isFacingAlready = playerObj.Direction == direction;
        if (isFacingAlready)
        {
            Move(direction, spriteGrid, gridPointer, context, playerCoord, playerObj, playerSprite);
        }
        else
        {
            Face(direction, spriteGrid, context, playerCoord, playerObj, playerSprite);
        }

        context.ShouldUpdate = true;
    }        

    private static void Move(Directions direction, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        var playerDestination = playerCoord + Utilities.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(playerDestination);

        var canMove = destinationGrid.Object.Id == ObjectIds.Empty;
        if (canMove)
        {
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
            spriteGrid.SetObjectSprite(playerCoord, playerDestination, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(playerDestination, playerObj);
        }
        context.IsIdle = false;
    }

    private static void Face(Directions direction, SpriteGrid spriteGrid, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = direction;
        var playerIdleSpriteRegion = RegionNames.ToPlayerIdleRegionName(direction);
        playerSprite = playerSprite.ConvertToStatic(playerIdleSpriteRegion);
        playerSprite.IdleName = playerIdleSpriteRegion;
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        context.IsIdle = true;
    }
}
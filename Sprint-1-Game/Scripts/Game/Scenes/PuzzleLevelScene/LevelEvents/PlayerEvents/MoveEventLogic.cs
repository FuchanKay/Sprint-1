using Scripts.GameComponents;
namespace Scripts.Game;

public class MoveEventLogic
{
    public static void Move(Directions direction, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, ObjectFactory objectFactory)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        var playerDestination = playerCoord + Utilities.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(playerDestination);
        
        var playerObj = gridPointer.GetObject(playerCoord);
        var isFacingAlready = playerObj.Direction == direction;
        if (isFacingAlready)
        {
            var canMove = destinationGrid.Object.Id == ObjectIds.Empty;
            if (canMove)
            {
                playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
                spriteGrid.SetObjectSprite(playerCoord, playerDestination, playerSprite);
                spriteGrid.StartObjectAnimation(playerSprite);

                gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
                gridPointer.SetObject(playerDestination, playerObj);
            }
            context.IsIdle = false;
        }
        else
        {
            playerObj.Direction = direction;
            var playerIdleSpriteRegion = RegionNames.ToPlayerIdleRegionName(direction);
            playerSprite = playerSprite.ConvertToStatic(playerIdleSpriteRegion);
            playerSprite.IdleName = playerIdleSpriteRegion;
            spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
            context.IsIdle = true;
        }

        context.ShouldUpdate = true;
    }
}
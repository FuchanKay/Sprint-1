namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        var playerDestination = playerCoord + Coordinate.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(playerDestination);
        
        var playerObj = gridPointer.GetObject(playerCoord);
        var isFacingDirection = playerObj.Direction == direction;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;
        if (isFacingDirection && isDestinationEmpty)
        {
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
            spriteGrid.SetObjectSprite(playerCoord, playerDestination, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(playerDestination, playerObj);
            context.IsIdle = false;
        }
        else if (!isFacingDirection)
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
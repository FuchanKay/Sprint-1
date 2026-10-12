namespace Scripts.Game;

public class MovePlayerSubEvent(Directions direction, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerDestination = playerCoord + Coordinate.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(playerDestination);
    
        var playerObj = gridPointer.GetObject(playerCoord);
        var isFacingDirection = playerObj.Direction == direction;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;
    
        if (isFacingDirection && isDestinationEmpty)
        {
            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
            spriteGrid.SetObjectSprite(playerCoord, playerDestination, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(playerDestination, playerObj);

            context.IsIdle = false;
        }
    }
}
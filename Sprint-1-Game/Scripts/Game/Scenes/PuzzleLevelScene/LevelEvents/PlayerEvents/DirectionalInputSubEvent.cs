namespace Scripts.Game;

public class DirectionalInputSubEvent(Directions direction, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);

        var destinationCoord = playerCoord + Coordinate.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(destinationCoord);
        var destinationObject = destinationGrid.Object;

        var destinationPlus1Object = gridPointer.GetObject(playerCoord + Coordinate.ToCoordinate(direction) * 2);

        if (CanPush(destinationGrid, destinationPlus1Object))
        {
            var objectSprite = spriteGrid.GetObjectSprite(destinationCoord);
            objectSprite.Duration = spriteGrid.PushDuration;
            var onePastDestinationCoord = playerCoord + Coordinate.ToCoordinate(direction) * 2;
            spriteGrid.SetObjectSprite(destinationCoord, onePastDestinationCoord, objectSprite);
            spriteGrid.StartObjectAnimation(objectSprite);

            gridPointer.SetObject(destinationCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(onePastDestinationCoord, destinationObject);

            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerPushAnimationName(direction));
            spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(destinationCoord, playerObj);
        }
        else if (CanMove(playerObj, destinationGrid))
        {
            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerMovingAnimationName(direction));
            spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(destinationCoord, playerObj);

            context.IsIdle = false;
        }
    }

    private bool CanMove(IObject playerObject, Grid destinationGrid)
    {
        var destinationTile = destinationGrid.Tile;

        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;
        var isFacingDirection = playerObject.Direction == direction;

        return destinationTile.IsWalkable && isFacingDirection && isDestinationEmpty;
    }

    private bool CanPush(Grid destinationGrid, IObject destinationPlus1Object)
    {
        var destinationObject = destinationGrid.Object;
        var destinationTile = destinationGrid.Tile;

        var destinationPlus1ObjectIsEmpty = destinationPlus1Object.Id == ObjectIds.Empty;

        return destinationObject.IsPushable && destinationTile.IsWalkable && destinationPlus1ObjectIsEmpty;
    }
}
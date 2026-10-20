namespace Scripts.Game;

public class DirectionalInputSubEvent(Directions direction, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var destinationCoord = playerCoord + Coordinate.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(destinationCoord);

        var playerObj = gridPointer.GetObject(playerCoord);
        var isFacingDirection = playerObj.Direction == direction;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;
        var destinationObject = destinationGrid.Object;

        var onePastDestinationCoord = destinationCoord + Coordinate.ToCoordinate(direction) * 2;

        var canPush = destinationObject.IsPushable && destinationGrid.Object.Id == ObjectIds.Empty;
        if (canPush)
        {
            // set object sprite moving towards dest
            var objectSprite = spriteGrid.GetObjectSprite(destinationCoord);
            // set duration for static sprite to move
            objectSprite.Duration = spriteGrid.PushDuration;
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

        if (isFacingDirection && isDestinationEmpty)
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
}
namespace Scripts.Game;

public class PushSubEvent(Directions direction, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        // get player object
        var directionAsCoordinate = Coordinate.ToCoordinate(direction);
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);

        // get object east of player
        var destinationCoord = playerCoord + directionAsCoordinate;
        var destinationGrid = gridPointer.GetGrid(destinationCoord);
        var destObj = destinationGrid.Object;

        // get grid east of east object
        var destCoordPlus1 = destinationCoord + directionAsCoordinate;
        var destGridPlus1 = gridPointer.GetGrid(destCoordPlus1);

        var canPush = destObj.IsPushable && destGridPlus1.Object.Id == ObjectIds.Empty;
        if (canPush)
        {
            // set object sprite moving towards dest
            var objectSprite = spriteGrid.GetObjectSprite(destinationCoord);
            // set duration for static sprite to move
            objectSprite.Duration = spriteGrid.PushDuration;
            spriteGrid.SetObjectSprite(destinationCoord, destCoordPlus1, objectSprite);
            spriteGrid.StartObjectAnimation(objectSprite);

            // set current object grid empty and set object one grid over
            gridPointer.SetObject(destinationCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(destCoordPlus1, destObj);

            // set player sprite pushing towards dest
            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerPushAnimationName(direction));
            spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            // set current player grid empty and set player one grid over
            gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
            gridPointer.SetObject(destinationCoord, playerObj);
        }
    }
}
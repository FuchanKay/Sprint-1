namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, bool lockMove, ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerDestination = playerCoord + Coordinate.ToCoordinate(direction);
        var destinationGrid = gridPointer.GetGrid(playerDestination);
        var playerObj = gridPointer.GetObject(playerCoord);

        var isFacingDirection = playerObj.Direction == direction;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;
        var isDestinationPushable = destinationGrid.Object.IsPushable;
        if (isFacingDirection && isDestinationEmpty)
        {
            SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack));
            SubEventStack.Push(new DirectionalInputSubEvent(direction, objectFactory));
        } else if(isFacingDirection && isDestinationPushable)
        {
            SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack));
            SubEventStack.Push(new PushSubEvent(direction, objectFactory));
        }
        else if (!isFacingDirection)
        {
            SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
        }
        if (!lockMove) SubEventStack.Push(new WarlockSwapEvent());
    }
}
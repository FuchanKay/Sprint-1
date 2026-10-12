namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, ObjectFactory objectFactory) : ILevelEvent
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
        if (isFacingDirection && isDestinationEmpty)
        {
            SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack));
            SubEventStack.Push(new MovePlayerSubEvent(direction, objectFactory, SubEventStack));
        }
        else if (!isFacingDirection)
        {
            SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
        }

        context.ShouldUpdate = true;
    }
    public bool IsFinished()
    {
        return SubEventStack.Count() == 0;
    }
}
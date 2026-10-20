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

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack));
        SubEventStack.Push(new DirectionalInputSubEvent(direction, objectFactory));
        SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
    }
}
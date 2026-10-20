namespace Scripts.Game;

public class MovePlayerEvent(Directions direction, bool lockMove, ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory));
        if (!lockMove) SubEventStack.Push(new DirectionalInputSubEvent(direction, objectFactory));
        SubEventStack.Push(new PlayerChangeDirectionSubEvent(direction));
    }
}
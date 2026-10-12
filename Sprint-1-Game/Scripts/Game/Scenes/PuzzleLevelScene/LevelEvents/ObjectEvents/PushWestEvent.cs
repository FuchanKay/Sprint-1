using Scripts.Game;

public class PushWestEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        PushEventLogic.Push(Directions.West, spriteGrid, gridPointer, context, objectFactory);
    }
    public bool IsFinished()
    {
        return SubEventStack.Count() == 0;
    }
}
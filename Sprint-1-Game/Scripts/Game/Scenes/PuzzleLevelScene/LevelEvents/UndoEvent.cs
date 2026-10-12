using Scripts.Game;

public class UndoEvent : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {

    }
    public bool IsFinished()
    {
        return SubEventStack.Count() == 0;
    }
}
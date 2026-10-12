namespace Scripts.Game;

public interface ILevelEvent
{
    public SubEventCallStack SubEventStack { get; }
    void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context);
    bool IsFinished();
}
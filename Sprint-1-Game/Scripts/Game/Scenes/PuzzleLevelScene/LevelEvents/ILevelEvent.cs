namespace Scripts.Game;

public interface ILevelEvent
{
    void Execute(GridPointer gridPointer, LevelContext context);
}
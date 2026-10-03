namespace Scripts.Game;

public interface ILevelEvent
{
    void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context);
}
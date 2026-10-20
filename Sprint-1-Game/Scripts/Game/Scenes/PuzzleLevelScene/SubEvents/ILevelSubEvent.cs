namespace Scripts.Game;

public interface ILevelSubEvent
{
    void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context);
}
using Scripts.Game;

public class SnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        // Only executes snap behavior for bomb objects
        new BombSnapEvent().Execute(spriteGrid, gridPointer, context);
        new TimedBombSnapEvent().Execute(spriteGrid, gridPointer, context);
    }
}
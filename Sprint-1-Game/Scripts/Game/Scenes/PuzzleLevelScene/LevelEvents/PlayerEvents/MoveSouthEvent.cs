namespace Scripts.Game;

public class MoveSouthEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        MoveEventLogic.Move(Directions.South, spriteGrid, gridPointer, context, objectFactory);
        context.ShouldUpdate = true;
    }
}
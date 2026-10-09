namespace Scripts.Game;

public class MoveWestEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        MoveEventLogic.Move(Directions.West, spriteGrid, gridPointer, context, objectFactory);
        context.ShouldUpdate = true;
    }
}
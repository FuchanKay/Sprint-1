namespace Scripts.Game;

public class MoveNorthEvent(ObjectFactory objectFactory) : ILevelEvent
{
    // TODO: address duplicate code between move events
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        MoveEventLogic.Move(Directions.North, spriteGrid, gridPointer, context, objectFactory);
        context.ShouldUpdate = true;
    }
}
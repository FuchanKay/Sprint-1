namespace Scripts.Game;

public class MoveEastEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        MoveEventLogic.Move(Directions.East, spriteGrid, gridPointer, context, objectFactory);
        context.ShouldUpdate = true;
    }
}
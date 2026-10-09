using Scripts.Game;

public class PushWestEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        PushEventLogic.Push(Directions.West, spriteGrid, gridPointer, context, objectFactory);
    }
}
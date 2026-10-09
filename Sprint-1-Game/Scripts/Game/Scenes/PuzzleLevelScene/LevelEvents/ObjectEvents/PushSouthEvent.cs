using Scripts.Game;

public class PushSouthEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        PushEventLogic.Push(Directions.South, spriteGrid, gridPointer, context, objectFactory);
    }
}
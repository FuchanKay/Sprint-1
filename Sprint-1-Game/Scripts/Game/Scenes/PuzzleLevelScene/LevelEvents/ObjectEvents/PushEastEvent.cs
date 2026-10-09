using Scripts.Game;
public class PushEastEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        PushEventLogic.Push(Directions.East, spriteGrid, gridPointer, context, objectFactory);
    }
}
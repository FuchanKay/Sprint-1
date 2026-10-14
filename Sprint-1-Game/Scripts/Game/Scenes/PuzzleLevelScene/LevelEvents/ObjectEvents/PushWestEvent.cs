using Scripts.Game;

public class PushWestEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        PushEventLogic.Push(Directions.West, spriteGrid, gridPointer, context, objectFactory);
    }
}
using Scripts.Game;

public class PushNorthEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        PushEventLogic.Push(Directions.North, spriteGrid, gridPointer, context, objectFactory);
    }
}
using Scripts.Game;

public class BombSnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        // Only executes snap behavior for bomb objects
        var bombPointers = gridPointer.GetObjectPointers(ObjectIds.Bomb);
        foreach (var bombPointer in bombPointers)
        {
            var bombCoord = bombPointer.Key;
            gridPointer.SetObject(bombCoord, new ExplosionObject());
        }
        new DestroyObjectEvent().Execute(spriteGrid, gridPointer, context);
    }
}
using System.Linq;
using Scripts.Game;

public class BombSnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var bombPointers = gridPointer.GetObjectPointers(ObjectIds.Bomb);
        var bombCoords = bombPointers.Keys.ToList();
        foreach (var bombCoord in bombCoords)
        {
            gridPointer.SetObject(bombCoord, new ExplosionObject());
        }
        if(bombCoords.Count > 0)
        {
            context.ShouldUpdate = true;
            new ExplodeObjectEvent().Execute(spriteGrid, gridPointer, context);
        }
    }
}
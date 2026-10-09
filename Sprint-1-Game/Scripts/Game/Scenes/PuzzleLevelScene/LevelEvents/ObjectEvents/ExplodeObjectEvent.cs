using System.Linq;
using Scripts.Game;

public class ExplodeObjectEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var explosionPointers = gridPointer.GetObjectPointers(ObjectIds.Explosion);
        var explosionCoords = explosionPointers.Keys.ToList();
        const int explosionRadius = 1;
        foreach (var explosionCoord in explosionCoords)
        {
            for (int x = -explosionRadius; x <= explosionRadius; x++)
            {
                for (int y = -explosionRadius; y <= explosionRadius; y++)
                {
                    var targetCoord = explosionCoord + new Coordinate(x, y);
                    var targetGrid = gridPointer.GetGrid(targetCoord);
                    if (targetGrid.Object.isDestructible)
                    {
                        gridPointer.SetObject(targetCoord, new EmptyObject());
                    }
                }
            }
            // gridPointer.SetObject(explosionCoord, new EmptyObject());
        }
        if (explosionCoords.Count > 0)
        {
            context.ShouldUpdate = true;
        }
    }
}
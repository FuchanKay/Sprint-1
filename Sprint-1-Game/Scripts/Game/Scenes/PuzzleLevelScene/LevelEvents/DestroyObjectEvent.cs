using Microsoft.Xna.Framework;
using Scripts.Game;

public class DestroyObjectEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var explosionPointers = gridPointer.GetObjectPointers(ObjectIds.Explosion);
        foreach (var explosionPointer in explosionPointers)
        {
            var explosionCoord = explosionPointer.Key;
            int explosionRadius = 1;
            for (int x = -explosionRadius; x <= explosionRadius; x++)
            {
                for (int y = -explosionRadius; y <= explosionRadius; y++)
                {
                    var targetCoord = explosionCoord + new Vector2(x, y);
                    var targetGrid = gridPointer.GetGrid(targetCoord);
                    bool isDestructible = DestructibleObjectIds.IsDefined(typeof(DestructibleObjectIds), targetGrid.Object.Id);
                    if (isDestructible)
                    {
                        gridPointer.SetObject(targetCoord, new EmptyObject());
                    }
                }
            }
            gridPointer.SetObject(explosionCoord, new EmptyObject());
        }
    }
}
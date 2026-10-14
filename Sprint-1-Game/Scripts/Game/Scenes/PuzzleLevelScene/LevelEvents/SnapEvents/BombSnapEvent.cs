using System.Collections.Generic;
using System.Linq;
using Scripts.Game;

public class BombSnapSubEvent(List<Coordinate> bombCoords, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        foreach (var bombCoord in bombCoords)
        {
            ExplodeBomb(bombCoord, spriteGrid, gridPointer, context);
        }
    }

    private void ExplodeBomb(Coordinate bombCoord, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
       
        gridPointer.SetObject(bombCoord, objectFactory.CreateEmpty());
        spriteGrid.RemoveObjectSprite(bombCoord);
        spriteGrid.PlayEffect(EffectIds.Explosion, bombCoord);

        const int explosionRadius = 1;
        for (int x = -explosionRadius; x <= explosionRadius; x++)
        {
            for (int y = -explosionRadius; y <= explosionRadius; y++)
            {
                var targetCoord = new Coordinate(bombCoord.X + x, bombCoord.Y + y);
                if (gridPointer.GetObject(targetCoord).IsDestructible)
                {
                    gridPointer.SetObject(targetCoord, objectFactory.CreateEmpty());
                    spriteGrid.RemoveObjectSprite(targetCoord);
                }
            }
        }
    }
}
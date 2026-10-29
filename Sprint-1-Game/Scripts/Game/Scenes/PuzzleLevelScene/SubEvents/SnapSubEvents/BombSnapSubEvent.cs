using System.Collections.Generic;
namespace Scripts.Game;

public class BombSnapSubEvent(List<Coordinate> bombCoords, ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    protected List<Coordinate> BombCoords = bombCoords;
    public virtual void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        foreach (var bombCoord in BombCoords)
        {
            ExplodeBomb(bombCoord, spriteGrid, gridPointer, context);
        }
    }

    protected void ExplodeBomb(Coordinate bombCoord, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {

        gridPointer.SetObject(bombCoord, ObjectFactory.CreateEmpty());
        spriteGrid.RemoveObjectSprite(bombCoord);
        spriteGrid.PlayEffect(EffectIds.Explosion, bombCoord);

        const int explosionRadius = 1;
        for (int x = -explosionRadius; x <= explosionRadius; x++)
        {
            for (int y = -explosionRadius; y <= explosionRadius; y++)
            {
                var targetCoord = new Coordinate(bombCoord.X + x, bombCoord.Y + y);
                var targetObj = gridPointer.GetObject(targetCoord);
                
                if (targetObj.Id == ObjectIds.Player)
                {
                    subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
                }
                else if (targetObj is IEnemy)
                {
                    subEventStack.Push(new EnemyDamageSubEvent(objectFactory, targetCoord));
                }
                else if (targetObj.IsDestructible)
                {
                    gridPointer.SetObject(targetCoord, ObjectFactory.CreateEmpty());
                    spriteGrid.RemoveObjectSprite(targetCoord);
                }
            }
        }
    }
}
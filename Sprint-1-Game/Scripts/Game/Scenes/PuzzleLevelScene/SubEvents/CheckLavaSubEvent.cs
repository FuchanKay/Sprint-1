using System.Collections.Generic;

namespace Scripts.Game;

public class CheckLavaSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var lavas = gridPointer.GetTilePointers(TileIds.Lava);
        foreach (var kvp in lavas)
        {
            var coord = kvp.Key;
            var obj = gridPointer.GetObject(coord);
            if (obj.Id == ObjectIds.Player)
            {
                subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
            }
            else if (obj.Id != ObjectIds.Empty)
            {
                gridPointer.SetObject(coord, objectFactory.CreateEmpty());
                spriteGrid.RemoveObjectSprite(coord);
                spriteGrid.PlayEffect(EffectIds.LavaExplosion, coord);
            }
        }
    }
}
using System.Linq;
using Scripts.Game;
using Scripts.GameComponents;

public class FrogHopSubEvent() : EnemiesMoveSubEvent
{
    public override void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var frogCoords = gridPointer.GetObjectPointers(ObjectIds.Frog).Keys.ToList();
        foreach (var frogCoord in frogCoords)
        {
            PathFindToPlayer(spriteGrid, gridPointer, frogCoord);
        }
    }
}
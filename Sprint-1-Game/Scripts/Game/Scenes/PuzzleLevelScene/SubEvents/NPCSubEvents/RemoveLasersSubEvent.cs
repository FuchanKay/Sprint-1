using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Scripts.GameComponents;
namespace Scripts.Game;

public class RemoveLasersSubEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        // Thread.Sleep(500);
        var effectSprites = spriteGrid.GetEffectSprites();
        var iteratingList = new Dictionary<Vector2, ISprite>(effectSprites);
        foreach (var pair in iteratingList)
        {
            var sprite = pair.Value;
            if(sprite.IdleName == RegionNames.LaserBeam) effectSprites.Remove(pair.Key);
        }

        var redLizCoords = gridPointer.GetObjectPointers(ObjectIds.RedLizard).Keys.ToList();
        var blueLizCoords = gridPointer.GetObjectPointers(ObjectIds.BlueLizard).Keys.ToList();
        List<Coordinate> lizardCoords = [..redLizCoords, ..blueLizCoords];

        foreach(var lizardCoord in lizardCoords)
        {
            var lizard = gridPointer.GetObject(lizardCoord);
            var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
            lizardSprite.SetState(RegionNames.ToIdleRegionName(lizard.Direction));
            lizardSprite.IdleName = RegionNames.ToIdleRegionName(lizard.Direction);
        }
    }
}
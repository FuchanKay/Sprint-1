using System.Collections.Generic;
using System.Linq;
namespace Scripts.Game;

public class BlueLizardAttackSubEvent(Stack<ILevelSubEvent> subEventStack, List<LevelStateHistory> levelStateHistory) : EnemiesAttackSubEvent(subEventStack, levelStateHistory)
{
    public override void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var lizardCoords = gridPointer.GetObjectPointers(ObjectIds.BlueLizard).Keys.ToList();
        foreach(var lizardCoord in lizardCoords)
        {
            LaserBlastAttack(spriteGrid, gridPointer, lizardCoord, EffectIds.BlueLaserBeam);
        }
    }
}
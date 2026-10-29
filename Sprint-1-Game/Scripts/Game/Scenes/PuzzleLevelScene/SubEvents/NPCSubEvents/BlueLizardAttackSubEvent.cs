using System.Collections.Generic;
using System.Linq;
using Scripts.GameComponents;
namespace Scripts.Game;

public class BlueLizardAttackSubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack, List<LevelStateHistory> levelStateHistory) : EnemiesAttackSubEvent(objectFactory, subEventStack, levelStateHistory)
{
    public override void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var lizardCoords = gridPointer.GetObjectPointers(ObjectIds.BlueLizard).Keys.ToList();
        foreach(var lizardCoord in lizardCoords)
        {
            LaserBlastAttack(spriteGrid, gridPointer, lizardCoord, EffectIds.BlueLaserBeam);
        }
    }
}
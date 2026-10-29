using System;
using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class SkeletonsAttackSubEvent(List<Coordinate> skeletonCoords, ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        foreach (var skeletonCoord in skeletonCoords)
        {
            TryAttack(skeletonCoord, gridPointer, spriteGrid);
        }
    }

    private void TryAttack(Coordinate skeletonCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        var playerCoord = Pathfinder.GetPlayerCoord(gridPointer, skeletonCoord);
        if(playerCoord != skeletonCoord)
        {
            // TODO: Change skeleton facing direction and sprite
            spriteGrid.PlayEffect(EffectIds.Slash, playerCoord);
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }
    }
}
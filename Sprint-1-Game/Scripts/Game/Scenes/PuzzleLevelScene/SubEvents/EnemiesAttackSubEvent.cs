using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EnemiesAttackSubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        
        var skeletonCoords = gridPointer.GetObjectPointers(ObjectIds.Skeleton).Keys.ToList();
        var batCoords = gridPointer.GetObjectPointers(ObjectIds.Bat).Keys.ToList();
        List<Coordinate> attackingEnemyCoords = [..skeletonCoords, ..batCoords]; 

        foreach (var attackingEnemyCoord in attackingEnemyCoords)
        {
            TryAttack(attackingEnemyCoord, gridPointer, spriteGrid);
        }
    }

    private void TryAttack(Coordinate enemyCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        List<Directions> directions = new List<Directions> { Directions.North, Directions.East, Directions.South, Directions.West };
        foreach (var direction in directions)
        {
            var targetCoord = enemyCoord + Coordinate.ToCoordinate(direction);
            var targetObj = gridPointer.GetObject(targetCoord);
            if (targetObj.Id == ObjectIds.Player)
            {
                spriteGrid.PlayEffect(EffectIds.Slash, targetCoord);
                subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
            }
        }
    }
}
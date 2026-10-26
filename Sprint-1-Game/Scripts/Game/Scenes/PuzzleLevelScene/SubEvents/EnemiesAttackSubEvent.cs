using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EnemiesAttackSubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        
        var skeletonCoords = gridPointer.GetObjectPointers(ObjectIds.Skeleton).Keys.ToList();
        var batCoords = gridPointer.GetObjectPointers(ObjectIds.Bat).Keys.ToList();
        var lizardCoords = gridPointer.GetObjectPointers(ObjectIds.RedLizard).Keys.ToList();
        List<Coordinate> omniDirEnemyCoords = [..skeletonCoords, ..batCoords]; 

        foreach (var attackingEnemyCoord in omniDirEnemyCoords)
        {
            OmniDirectionalAttack(attackingEnemyCoord, gridPointer, spriteGrid);
        }

        var zombieCoords = gridPointer.GetObjectPointers(ObjectIds.Zombie).Keys.ToList();

        foreach (var zombieCoord in zombieCoords)
        {
            AttackFront(zombieCoord, gridPointer, spriteGrid);
        }

        foreach (var lizardCoord in lizardCoords)
        {
            FireBlastAttack(lizardCoord, gridPointer, spriteGrid);
        }
    }

    private void OmniDirectionalAttack(Coordinate enemyCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
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

    private void AttackFront(Coordinate enemyCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        var enemy = gridPointer.GetObject(enemyCoord);
        var direction = enemy.Direction;
        var targetCoord = enemyCoord + Coordinate.ToCoordinate(direction);
        var targetObj = gridPointer.GetObject(targetCoord);
        if (targetObj.Id == ObjectIds.Player)
        {
            spriteGrid.PlayEffect(EffectIds.Slash, targetCoord);
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }
    }

    private void FireBlastAttack(Coordinate enemyCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        var enemy = gridPointer.GetObject(enemyCoord);
        var direction = enemy.Direction;
        Stack<Coordinate> lineOfSight = GetLineOfSight(gridPointer, enemyCoord, direction);
        bool seesPlayer = gridPointer.GetObject(lineOfSight.Peek()).Id == ObjectIds.Player;

        if (seesPlayer)
        {
            foreach (var coord in lineOfSight)
            {
                spriteGrid.PlayEffect(EffectIds.LavaExplosion, coord);
            }
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }

    }

    private Stack<Coordinate> GetLineOfSight(GridPointer gridPointer, Coordinate enemyCoord, Directions direction)
    {
        Stack<Coordinate> lineOfSight = [];
        var nextCoord = enemyCoord + Coordinate.ToCoordinate(direction);
        lineOfSight.Push(nextCoord);

        while(gridPointer.GetObject(nextCoord).Id == ObjectIds.Empty)
        {
            nextCoord += Coordinate.ToCoordinate(direction);
            lineOfSight.Push(nextCoord);
        }

        return lineOfSight;
    }
}
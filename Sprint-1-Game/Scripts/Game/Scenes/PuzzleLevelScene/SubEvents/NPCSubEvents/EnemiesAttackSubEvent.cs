using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Scripts.GameComponents;
using System.Linq;

namespace Scripts.Game;

public class EnemiesAttackSubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack, List<LevelStateHistory> levelStateHistory) : ILevelSubEvent
{
    public virtual void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
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
            if (ShouldLaserBlast(spriteGrid, gridPointer, levelStateHistory, lizardCoord))
            {
                LaserBlastAttack(spriteGrid, gridPointer, lizardCoord, EffectIds.RedLaserBeam);
            }
        }
    }

    private void OmniDirectionalAttack(Coordinate enemyCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        var playerCoord = Pathfinder.GetPlayerCoord(gridPointer, enemyCoord);
        if(playerCoord != enemyCoord)
        {
            spriteGrid.PlayEffect(EffectIds.Slash, playerCoord);
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
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

    protected void LaserBlastAttack(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate startCoord, EffectIds laserColorId)
    {
        var direction = gridPointer.GetObject(startCoord).Direction;
        var laserPath = Pathfinder.GetLaserPath(gridPointer, startCoord, direction);
        var endCoord = laserPath.Peek();

        var lizard = gridPointer.GetObject(startCoord);
            
        var lizardSprite = spriteGrid.GetObjectSprite(startCoord);
        var mouthOpen = RegionNames.ToLizardMouthOpenName(direction);
        lizardSprite.SetState(mouthOpen);
        lizardSprite.IdleName = mouthOpen;

        // N/S -> rotation = 0
        // E/W -> rotation = 90
        var rotation = MathHelper.ToRadians(((int)direction % 2) * 90);

        var dirCoord = startCoord + Coordinate.ToCoordinate(direction);
        var startDirCoord = dirCoord;
        var dirGrid = gridPointer.GetGrid(dirCoord);
        Console.WriteLine($"{endCoord}");
        while (dirCoord != endCoord)
        {
            spriteGrid.AddIterableEffectSprite(laserColorId, startDirCoord, dirCoord, rotation);

            if(dirGrid.Object.Id == ObjectIds.Player)
            {
                subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
            } else if (dirGrid.Object is IEnemy)
            {
                subEventStack.Push(new EnemyDamageSubEvent(objectFactory, dirCoord));
            }
            else if (dirGrid.Object.IsDestructible)
            {
                gridPointer.SetObject(dirCoord, ObjectFactory.CreateEmpty());
                spriteGrid.RemoveObjectSprite(dirCoord);
            }

            dirCoord += Coordinate.ToCoordinate(direction);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }

        if(dirGrid.Object.IsDestructible)
        {
            gridPointer.SetObject(dirCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(dirCoord);
        }

        if(dirGrid.Object.Id == ObjectIds.Player)
        {
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }

        var laserImpactEffect = EffectIds.RedLaserImpact;
        if(lizard.Id == ObjectIds.BlueLizard) laserImpactEffect = EffectIds.BlueLaserImpact;
        spriteGrid.AddIterableEffectSprite(laserImpactEffect, startDirCoord, dirCoord, 0);
        spriteGrid.StartIterableEffect(startDirCoord);

        subEventStack.Push(new RemoveLasersSubEvent());
    }

    private static bool ShouldLaserBlast(SpriteGrid spriteGrid, GridPointer gridPointer, List<LevelStateHistory> levelStateHistory, Coordinate lizardCoord)
    {
        var lizardDirection = gridPointer.GetObject(lizardCoord).Direction;

        Stack<Coordinate> lineOfSight = Pathfinder.GetLineOfSight(gridPointer, lizardCoord, lizardDirection);
        var currentObjAhead = gridPointer.GetObject(lineOfSight.Peek());
        var seesPlayer = currentObjAhead.Id == ObjectIds.Player;
        var seesDestructible = currentObjAhead.IsDestructible;
        var seesEnemy = currentObjAhead is IEnemy;

        var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
        var isIdle = lizardSprite.IdleName.Contains("Idle");

       return (seesPlayer || seesDestructible || seesEnemy) && isIdle;
    }
}
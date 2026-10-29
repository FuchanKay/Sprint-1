using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EnemiesMoveSubEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var batCoords = gridPointer.GetObjectPointers(ObjectIds.Bat).Keys.ToList();
        var frogCoords = gridPointer.GetObjectPointers(ObjectIds.Frog).Keys.ToList();
        var zombieCoords = gridPointer.GetObjectPointers(ObjectIds.Zombie).Keys.ToList();
        foreach (var batCoord in batCoords)
        {
            PathFindToPlayer(spriteGrid, gridPointer, batCoord);
        }
        foreach(var frogCoord in frogCoords)
        {
            MoveRandom(spriteGrid, gridPointer,  frogCoord);
        }
        foreach (var zombieCoord in zombieCoords)
        {
            MoveHorizontal(spriteGrid, gridPointer, zombieCoord);
        }
    }

    private void MoveRandom(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate enemyCoord)
    {
        List<Directions> validDirections = Pathfinder.GetValidDirections(gridPointer, enemyCoord);
        int randomInt = Random.Shared.Next(0, validDirections.Count);
        var destination = validDirections[randomInt];
        if(gridPointer.GetObject(enemyCoord + Coordinate.ToCoordinate(destination)).Id == ObjectIds.Player) return;
        MoveEnemySprite(spriteGrid, gridPointer, enemyCoord, destination);
    }

    private void MoveHorizontal(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate enemyCoord)
    {
        var enemyObj = gridPointer.GetObject(enemyCoord);
        bool isFacingHorizontal = enemyObj.Direction == Directions.East || enemyObj.Direction == Directions.West;
        if (!isFacingHorizontal) enemyObj.Direction = Directions.East;

        var oppositeDirection = Directions.West;
        if (enemyObj.Direction == Directions.West) oppositeDirection = Directions.East; 

        var destinationCoord = enemyCoord + Coordinate.ToCoordinate(enemyObj.Direction);
        var destinationGrid = gridPointer.GetGrid(destinationCoord);
        var oppositeCoord = enemyCoord + Coordinate.ToCoordinate(oppositeDirection);
        var oppositeGrid = gridPointer.GetGrid(oppositeCoord);

        if (!Pathfinder.CanMove(destinationGrid))
        {
            if (!Pathfinder.CanMove(oppositeGrid) || gridPointer.GetObject(destinationCoord).Id == ObjectIds.Player) return;
            enemyObj.Direction = oppositeDirection;
        }
        MoveEnemySprite(spriteGrid, gridPointer, enemyCoord, enemyObj.Direction);
    }

    private void PathFindToPlayer(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate enemyCoord)
    {
        var destinationCoord = Pathfinder.FindNextPointTowardsPlayer(gridPointer, enemyCoord);
        Console.WriteLine($"Moving to {destinationCoord}");

        var directionCoord = enemyCoord - destinationCoord;
        var direction = Directions.North;
        if(directionCoord.X == -1) direction = Directions.East;
        else if(directionCoord.X == 1) direction = Directions.West;
        else if(directionCoord.Y == -1) direction = Directions.South;

        if (destinationCoord.X != -1 && destinationCoord.Y != -1)
        {
            MoveEnemySprite(spriteGrid, gridPointer, enemyCoord, direction);
        } 
    }

    // Overload method to handle the frogs special directional movement sprites.
    private void MoveEnemySprite(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate enemyCoord, Directions direction)
    {
        var enemySprite = spriteGrid.GetObjectSprite(enemyCoord);
        var destinationCoord = enemyCoord + Coordinate.ToCoordinate(direction);
        var enemyObj = gridPointer.GetObject(enemyCoord);

        var movingAnimation = AnimationNames.BatFly;
        var newIdleName = RegionNames.Bat;

        switch (enemyObj.Id)
        {  
            case ObjectIds.Frog:
                movingAnimation = AnimationNames.ToFrogJumpAnimationName(direction);
                newIdleName = RegionNames.ToFrogIdleRegionName(direction);
                break;
            case ObjectIds.Zombie:
                movingAnimation = AnimationNames.ToZombieWalkAnimationName(direction);
                newIdleName = RegionNames.ToZombieIdleRegionName(direction);
                break;
            default:
                break;
        }

        enemySprite = enemySprite.ConvertToAnimated(movingAnimation);
        spriteGrid.SetObjectSprite(enemyCoord, destinationCoord, enemySprite);
        spriteGrid.StartObjectAnimation(enemyCoord);
        enemySprite.IdleName = newIdleName;

        gridPointer.SetObject(enemyCoord, ObjectFactory.CreateEmpty());
        gridPointer.SetObject(destinationCoord, enemyObj); 
    }
}
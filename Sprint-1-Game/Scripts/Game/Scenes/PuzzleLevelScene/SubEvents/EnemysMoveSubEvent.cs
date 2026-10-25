using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Scripts.GameComponents;

namespace Scripts.Game;

public class EnemiesMoveSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var batCoords = gridPointer.GetObjectPointers(ObjectIds.Bat).Keys.ToList();
        var frogCoords = gridPointer.GetObjectPointers(ObjectIds.Frog).Keys.ToList();
        foreach (var batCoord in batCoords)
        {
            PathFindToPlayer(spriteGrid, gridPointer, AnimationNames.BatFly, batCoord);
        }
        foreach(var frogCoord in frogCoords)
        {
            MoveHorizontal(spriteGrid, gridPointer, AnimationNames.FrogJumpSouth, frogCoord);
        }
    }

    private void MoveRandom(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord)
    {
        List<Directions> validDirections = Pathfinder.GetValidDirections(gridPointer, enemyCoord);
        int randomInt = Random.Shared.Next(0, validDirections.Count);
        var destination = validDirections[randomInt];
        if (gridPointer.GetObject(enemyCoord).Id == ObjectIds.Frog) MoveEnemySprite(spriteGrid, gridPointer, enemyCoord, destination);
        else
        {
            var destinationCoord = enemyCoord + Coordinate.ToCoordinate(destination);
            MoveEnemySprite(spriteGrid, gridPointer, movingAnimation, enemyCoord, destinationCoord);
        }
    }

    private void MoveHorizontal(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord)
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
        
        if (!Pathfinder.CanMove(destinationGrid) && Pathfinder.CanMove(oppositeGrid))
        {
            enemyObj.Direction = oppositeDirection;
            destinationCoord = oppositeCoord;
        }
        else if (!Pathfinder.CanMove(oppositeGrid)) return;

        if (gridPointer.GetObject(enemyCoord).Id == ObjectIds.Frog) MoveEnemySprite(spriteGrid, gridPointer, enemyCoord, enemyObj.Direction);
        else
        {
            MoveEnemySprite(spriteGrid, gridPointer, movingAnimation, enemyCoord, destinationCoord);
        }
    }

    private void PathFindToPlayer(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord)
    {
        var destinationCoord = Pathfinder.FindNextPointTowardsPlayer(gridPointer, enemyCoord);
        Console.WriteLine($"Moving to {destinationCoord}");
        if (destinationCoord.X != -1 && destinationCoord.Y != -1)
        {
            MoveEnemySprite(spriteGrid, gridPointer, movingAnimation, enemyCoord, destinationCoord);
        } 
    }

    private void MoveEnemySprite(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord, Coordinate destinationCoord)
    {
        var enemySprite = spriteGrid.GetObjectSprite(enemyCoord);
        enemySprite = enemySprite.ConvertToAnimated(movingAnimation);
        spriteGrid.SetObjectSprite(enemyCoord, destinationCoord, enemySprite);
        spriteGrid.StartObjectAnimation(enemySprite);

        var enemyObj = gridPointer.GetObject(enemyCoord);
        gridPointer.SetObject(enemyCoord, objectFactory.CreateEmpty());
        gridPointer.SetObject(destinationCoord, enemyObj); 
    }

    // Overload method to handle the frogs special directional movement sprites.
    private void MoveEnemySprite(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate enemyCoord, Directions direction)
    {
        var enemySprite = spriteGrid.GetObjectSprite(enemyCoord);
        var destinationCoord = enemyCoord + Coordinate.ToCoordinate(direction);

        var movingAnimation = AnimationNames.ToFrogJumpAnimationName(direction);
        enemySprite = enemySprite.ConvertToAnimated(movingAnimation);
        spriteGrid.SetObjectSprite(enemyCoord, destinationCoord, enemySprite);
        spriteGrid.StartObjectAnimation(enemySprite);
        enemySprite.IdleName = RegionNames.ToFrogIdleRegionName(direction);

        var enemyObj = gridPointer.GetObject(enemyCoord);
        gridPointer.SetObject(enemyCoord, objectFactory.CreateEmpty());
        gridPointer.SetObject(destinationCoord, enemyObj); 
    }
}
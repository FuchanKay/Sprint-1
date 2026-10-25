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
            MoveEnemyRandom(spriteGrid, gridPointer, AnimationNames.FrogJumpSouth, frogCoord);
        }
    }

    private void MoveEnemyRandom(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord)
    {
        List<Directions> validDirections = GetValidDirections(gridPointer, enemyCoord);
        int randomInt = Random.Shared.Next(0, validDirections.Count);
        var destination = validDirections[randomInt];
        if (gridPointer.GetObject(enemyCoord).Id == ObjectIds.Frog) MoveEnemySprite(spriteGrid, gridPointer, enemyCoord, destination);
        else
        {
            var destinationCoord = enemyCoord + Coordinate.ToCoordinate(destination);
            MoveEnemySprite(spriteGrid, gridPointer, movingAnimation, enemyCoord, destinationCoord);
        }
    }

    private void PathFindToPlayer(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord)
    {
        var destinationCoord = FindNextPointTowardsPlayer(gridPointer, enemyCoord);
        Console.WriteLine($"Moving to {destinationCoord}");
        if (destinationCoord.X != -1 && destinationCoord.Y != -1)
        {
            MoveEnemySprite(spriteGrid, gridPointer, movingAnimation, enemyCoord, destinationCoord);
        } 
    }

    private Coordinate FindNextPointTowardsPlayer(GridPointer gridPointer, Coordinate enemyCoord)
    {
        var nullCoord = new Coordinate
        {
            X = -1,
            Y = -1
        };
        var playerCoord = gridPointer.GetPlayerCoord();
        var startingNeighbors = GetValidNeighbors(gridPointer, enemyCoord);
        if (startingNeighbors.Contains(playerCoord)) return nullCoord;

        var coordinateQueue = new Queue<Coordinate>();
        var nextMove = new Dictionary<Coordinate, Coordinate>();
        var visitedCoords = new HashSet<Coordinate> {enemyCoord};
        coordinateQueue.Enqueue(enemyCoord);

        while(coordinateQueue.Count > 0)
        {
            var current = coordinateQueue.Dequeue();
            List<Coordinate> neighbors = GetValidNeighbors(gridPointer, current);
            foreach (var next in neighbors)
            {
                if(!visitedCoords.Add(next)) continue;

                if(current.X == enemyCoord.X && current.Y == enemyCoord.Y)
                {
                    nextMove[next] = next;
                } else
                {
                    nextMove[next] = nextMove[current];
                }

                if(next.X == playerCoord.X && next.Y == playerCoord.Y) return nextMove[next];
                coordinateQueue.Enqueue(next); 
            }
        }
        return nullCoord;
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

    private List<Coordinate> GetValidNeighbors(GridPointer gridPointer, Coordinate coord)
    {
        List<Directions> directions = [Directions.North, Directions.East, Directions.South, Directions.West];
        List<Coordinate> validCoordinates = [];
        foreach (var direction in directions)
        {
            var targetGrid = gridPointer.GetGrid(coord + Coordinate.ToCoordinate(direction));
            var targetCoord = coord + Coordinate.ToCoordinate(direction);
            if(CanMove(targetGrid, direction))
            {
                validCoordinates.Add(targetCoord);
            }
        }
        return validCoordinates;
    }

    private List<Directions> GetValidDirections(GridPointer gridPointer, Coordinate coord)
    {
        List<Directions> directions = [Directions.North, Directions.East, Directions.South, Directions.West];
        List<Directions> validDirections = [];
        foreach (var direction in directions)
        {
            var targetGrid = gridPointer.GetGrid(coord + Coordinate.ToCoordinate(direction));
            if(CanMove(targetGrid, direction))
            {
                validDirections.Add(direction);
            }
        }
        return validDirections;
    }

    private bool CanMove(Grid destinationGrid, Directions direction)
    {
        var destinationTile = destinationGrid.Tile;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty || destinationGrid.Object.Id == ObjectIds.Player;

        return destinationTile.IsWalkable && destinationTile.Id != TileIds.Lava && isDestinationEmpty;
    }
}
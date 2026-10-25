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
        foreach (var batCoord in batCoords)
        {
            PathFindToPlayer(spriteGrid, gridPointer, AnimationNames.BatFly, batCoord);
        }
    }

    private void MoveEnemyRandom(SpriteGrid spriteGrid, GridPointer gridPointer, string movingAnimation, Coordinate enemyCoord)
    {
        List<Coordinate> validCoordinates = GetValidNeighbors(gridPointer, enemyCoord);
        int randomInt = Random.Shared.Next(0, validCoordinates.Count);
        var destinationCoord = validCoordinates[randomInt];
        MoveEnemySprite(spriteGrid, gridPointer, movingAnimation, enemyCoord, destinationCoord);
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

    private bool CanMove(Grid destinationGrid, Directions direction)
    {
        var destinationTile = destinationGrid.Tile;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty || destinationGrid.Object.Id == ObjectIds.Player;

        return destinationTile.IsWalkable && destinationTile.Id != TileIds.Lava && isDestinationEmpty;
    }
}
using System.Collections.Generic;

namespace Scripts.Game;

public class Pathfinder()
{
    public static Coordinate FindNextPointTowardsPlayer(GridPointer gridPointer, Coordinate enemyCoord)
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

    public static List<Directions> GetValidDirections(GridPointer gridPointer, Coordinate coord)
    {
        List<Directions> directions = [Directions.North, Directions.East, Directions.South, Directions.West];
        List<Directions> validDirections = [];
        foreach (var direction in directions)
        {
            var targetGrid = gridPointer.GetGrid(coord + Coordinate.ToCoordinate(direction));
            if(CanMove(targetGrid))
            {
                validDirections.Add(direction);
            }
        }
        return validDirections;
    }

    public static bool CanMove(Grid destinationGrid)
    {
        var destinationTile = destinationGrid.Tile;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;

        return destinationTile.IsWalkable && destinationTile.Id != TileIds.Lava && isDestinationEmpty;
    }

    private static List<Coordinate> GetValidNeighbors(GridPointer gridPointer, Coordinate coord)
    {
        List<Directions> directions = [Directions.North, Directions.East, Directions.South, Directions.West];
        List<Coordinate> validCoordinates = [];
        foreach (var direction in directions)
        {
            var targetGrid = gridPointer.GetGrid(coord + Coordinate.ToCoordinate(direction));
            var targetCoord = coord + Coordinate.ToCoordinate(direction);
            if(CanMoveTowardPlayer(targetGrid))
            {
                validCoordinates.Add(targetCoord);
            }
        }
        return validCoordinates;
    }
    
    public static bool CanMoveTowardPlayer(Grid destinationGrid)
    {
        var destinationTile = destinationGrid.Tile;
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty || destinationGrid.Object.Id == ObjectIds.Player;

        return destinationTile.IsWalkable && destinationTile.Id != TileIds.Lava && isDestinationEmpty;
    }
}
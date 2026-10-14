using System;
using System.Collections.Generic;
namespace Scripts.Game;

public class GridPointer
{
    private readonly Dictionary<Coordinate, Grid> CoordToGrid = [];
    private readonly Dictionary<ObjectIds, Dictionary<Coordinate, IObject>> ObjectPointersCollection = [];
    private readonly Dictionary<TileIds, Dictionary<Coordinate, ITile>> TilePointersCollection = [];

    public void Init()
    {
        foreach (var id in Enum.GetValues<ObjectIds>())
        {
            ObjectPointersCollection.Add(id, []);
        }
        foreach (var id in Enum.GetValues<TileIds>())
        {
            TilePointersCollection.Add(id, []);
        }
    }

    public Dictionary<Coordinate, Grid> DuplicateAllGrids()
    {
        Dictionary<Coordinate, Grid> all = [];

        foreach(var coordAndGrid in CoordToGrid)
        {
            var coord = coordAndGrid.Key;
            var grid = coordAndGrid.Value;
            all.Add(coord, grid);
        }

    }

    public void SetGrid(Coordinate coord, Grid grid)
    {
        if (CoordToGrid.TryGetValue(coord, out var oldGrid))
        {
            var oldObjPointer = ObjectPointersCollection[oldGrid.Object.Id];
            var oldTilePointer = TilePointersCollection[oldGrid.Tile.Id];

            oldObjPointer.Remove(coord);
            oldTilePointer.Remove(coord);
            CoordToGrid.Remove(coord);
        }

        var newObjPointers = ObjectPointersCollection[grid.Object.Id];
        var newTilePointers = TilePointersCollection[grid.Tile.Id];

        newObjPointers[coord] = grid.Object;
        newTilePointers[coord] = grid.Tile;
        CoordToGrid[coord] = grid;
    }

    public void SetObject(Coordinate coord, IObject obj)
    {
        var gridToSetObj = CoordToGrid[coord];
        var oldObjPointers = ObjectPointersCollection[gridToSetObj.Object.Id];
        oldObjPointers.Remove(coord);

        var newObjPointers = ObjectPointersCollection[obj.Id];
        newObjPointers[coord] = obj;

        CoordToGrid[coord].Object = obj;
    }

    public void SetTile(Coordinate coord, ITile tile)
    {
        var gridToSetTile = CoordToGrid[coord];
        var oldTilePointers = TilePointersCollection[gridToSetTile.Tile.Id];
        oldTilePointers.Remove(coord);

        var newTilePointers = TilePointersCollection[tile.Id];
        newTilePointers[coord] = tile;

        CoordToGrid[coord].Tile = tile;
    }

    public Grid GetGrid(Coordinate coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return grid;
    }

    public IObject GetObject(Coordinate coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return grid.Object;
    }

    public ITile GetTile(Coordinate coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return grid.Tile;
    }

    public Dictionary<Coordinate, IObject> GetObjectPointers(ObjectIds id)
    {
        return ObjectPointersCollection[id];
    }

    public Dictionary<Coordinate, ITile> GetTilePointers(TileIds id)
    {
        return TilePointersCollection[id];
    }

    public Coordinate GetPlayerCoord()
    {
        var playerPointers = ObjectPointersCollection[ObjectIds.Player];
        if (playerPointers.Count != 1)
        {
            throw new ArgumentException("Number of player objects is not equal to one");
        }
        foreach (var coordToObj in playerPointers)
        {
            return coordToObj.Key;
        }
        return Coordinate.Zero;
    }
}
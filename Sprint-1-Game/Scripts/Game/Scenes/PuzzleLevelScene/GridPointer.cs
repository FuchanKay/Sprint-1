using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Scripts.Game;

public class GridPointer
{
    private readonly Dictionary<Vector2, Grid> CoordToGrid = [];
    private readonly Dictionary<ObjectIds, Dictionary<Vector2, IObject>> ObjectPointers = [];
    private readonly Dictionary<TileIds, Dictionary<Vector2, ITile>> TilePointers = [];

    public void Init()
    {
        ObjectPointers.Add(ObjectIds.Empty, []);
        ObjectPointers.Add(ObjectIds.Rock, []);
        ObjectPointers.Add(ObjectIds.Player, []);

        TilePointers.Add(TileIds.Empty, []);
    }

    public void SetGrid(Vector2 coord, Grid grid)
    {
        if (CoordToGrid.TryGetValue(coord, out var oldGrid))
        {
            var oldObjPointer = ObjectPointers[oldGrid.Object.Id];
            var oldTilePointer = TilePointers[oldGrid.Tile.Id];

            oldObjPointer.Remove(coord);
            oldTilePointer.Remove(coord);
            CoordToGrid.Remove(coord);
        }

        var newObjPointer = ObjectPointers[grid.Object.Id];
        var newTilePointer = TilePointers[grid.Tile.Id];

        newObjPointer[coord] = grid.Object;
        newTilePointer[coord] = grid.Tile;
        CoordToGrid[coord] = grid;
    }

    public void SetObject(Vector2 coord, IObject obj)
    {
        var gridToSetObj = CoordToGrid[coord];
        var oldObjPointer = ObjectPointers[gridToSetObj.Object.Id];
        oldObjPointer.Remove(coord);

        var newObjPointer = ObjectPointers[obj.Id];
        newObjPointer[coord] = obj;

        CoordToGrid[coord].Object = obj;
    }

    public void SetTile(Vector2 coord, ITile tile)
    {
        var gridToSetTile = CoordToGrid[coord];
        var oldTilePointer = TilePointers[gridToSetTile.Tile.Id];
        oldTilePointer.Remove(coord);

        var newTilePointer = TilePointers[tile.Id];
        newTilePointer[coord] = tile;

        CoordToGrid[coord].Tile = tile;
    }

    public Grid GetGrid(Vector2 coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return grid;
    }

    public IObject GetObject(Vector2 coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return grid.Object;
    }

    public ITile GetTile(Vector2 coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return grid.Tile;
    }

    public Dictionary<Vector2, IObject> GetObjectPointer(ObjectIds id)
    {
        return ObjectPointers[id];
    }

    public Dictionary<Vector2, ITile> GetTilePointer(TileIds id)
    {
        return TilePointers[id];
    }

    public Vector2 GetPlayerCoord()
    {
        var playerPointers = ObjectPointers[ObjectIds.Player];
        foreach (var coordToObj in playerPointers)
        {
            return coordToObj.Key;
        }
        return new Vector2(0, 0);
    }
}
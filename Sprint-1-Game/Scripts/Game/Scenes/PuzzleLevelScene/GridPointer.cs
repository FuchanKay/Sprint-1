using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Scripts.Game;

public class GridPointer
{
    private readonly Dictionary<Vector2, Grid> CoordToGrid = [];
    private readonly Dictionary<ObjectIds, Dictionary<Vector2, IObject>> ObjectPointersCollection = [];
    private readonly Dictionary<TileIds, Dictionary<Vector2, ITile>> TilePointersCollection = [];

    public void Init()
    {
        ObjectPointersCollection.Add(ObjectIds.Empty, []);
        ObjectPointersCollection.Add(ObjectIds.Rock, []);
        ObjectPointersCollection.Add(ObjectIds.Player, []);
        ObjectPointersCollection.Add(ObjectIds.Wall, []);
        ObjectPointersCollection.Add(ObjectIds.Bomb, []);
        ObjectPointersCollection.Add(ObjectIds.Explosion, []);
        ObjectPointersCollection.Add(ObjectIds.TimedBomb, []);

        TilePointersCollection.Add(TileIds.Empty, []);
        TilePointersCollection.Add(TileIds.Brick, []);
        TilePointersCollection.Add(TileIds.Grass, []);
        TilePointersCollection.Add(TileIds.Water, []);
    }

    public void SetGrid(Vector2 coord, Grid grid)
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

    public void SetObject(Vector2 coord, IObject obj)
    {
        var gridToSetObj = CoordToGrid[coord];
        var oldObjPointers = ObjectPointersCollection[gridToSetObj.Object.Id];
        oldObjPointers.Remove(coord);

        var newObjPointers = ObjectPointersCollection[obj.Id];
        newObjPointers[coord] = obj;

        CoordToGrid[coord].Object = obj;
    }

    public void SetTile(Vector2 coord, ITile tile)
    {
        var gridToSetTile = CoordToGrid[coord];
        var oldTilePointers = TilePointersCollection[gridToSetTile.Tile.Id];
        oldTilePointers.Remove(coord);

        var newTilePointers = TilePointersCollection[tile.Id];
        newTilePointers[coord] = tile;

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

    public Dictionary<Vector2, IObject> GetObjectPointers(ObjectIds id)
    {
        return ObjectPointersCollection[id];
    }

    public Dictionary<Vector2, ITile> GetTilePointers(TileIds id)
    {
        return TilePointersCollection[id];
    }

    public Vector2 GetPlayerCoord()
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
        return new Vector2(0, 0);
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GridPointer
{
    private readonly Dictionary<Vector2, Grid> CoordToGrid = [];

    public void SetGrid(Vector2 coord, Grid grid)
    {
        //TODO: make sure that each thing is pointed to
        if (!CoordToGrid.TryAdd(coord, grid))
        {
            CoordToGrid[coord] = grid;
        }
    }

    public void SetObject(Vector2 coord, IObject obj)
    {
        
    }

    public void SetTile(Vector2 coord, ITile tile)
    {
        
    }

    public Grid GetGrid(Vector2 coord)
    {
        if (!CoordToGrid.TryGetValue(coord, out var grid))
        {
            //TODO: Make sure this is an empty grid
            return new Grid();
        }
        return grid;
    }

}
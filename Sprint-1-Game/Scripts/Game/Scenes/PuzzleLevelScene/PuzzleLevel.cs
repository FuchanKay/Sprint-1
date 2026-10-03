using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class PuzzleLevel(ObjectFactory objectFactory, TileFactory tileFactory)
{
    private List<ILevelEvent> EventQueue = [];
    private readonly LevelContext Context = new()
    {
        LevelWidth = 10,
        LevelHeight = 10,
        IsIdle = true
    };
    private readonly GridPointer GridPointer = new();
    private readonly int MaxEventsQueued = 3; 
    public bool IsIdle = true;

    public void Init()
    {
        GridPointer.Init();
        for (int i = 0; i < Context.LevelWidth * Context.LevelHeight; i++)
        {
            var x = i % Context.LevelWidth;
            var y = i / Context.LevelHeight;
            var coord = new Vector2(x, y);

            var emptyGrid = new Grid
            {
                Object = objectFactory.CreateEmpty(),
                Tile = tileFactory.CreateEmptyTile()
            };

            GridPointer.SetGrid(coord, emptyGrid);
        }

        GridPointer.SetObject(new Vector2(0, 0), objectFactory.CreateRock());
        GridPointer.SetObject(new Vector2(1, 0), objectFactory.CreatePlayer());
    }

    public void Draw(SpriteBatch sb)
    {
        for (int i = 0; i < Context.LevelWidth * Context.LevelHeight; i++)
        {
            var x = i % Context.LevelWidth;
            var y = i / Context.LevelHeight;
            var coord = new Vector2(x, y);

            var grid = GridPointer.GetGrid(coord);
            var obj = grid.Object;
            var tile = grid.Tile;
            if (tile.Id != TileIds.Empty)
            {
                tile.Sprite.Position = new Vector2(x * 50, y * 50);
                tile.Sprite.Draw(sb);
            }
            if (obj.Id != ObjectIds.Empty)
            {
                obj.Sprite.Position = new Vector2(x * 50, y * 50);
                obj.Sprite.Draw(sb);
            }
        }
    }

    public void EnqueueEvent(ILevelEvent levelEvent)
    {
        if (EventQueue.Count < MaxEventsQueued)
        {
            EventQueue.Add(levelEvent);
        }
    }

    public void ExecuteEvent()
    {
        if (Context.IsIdle && EventQueue.Count > 0)
        {
            var first = EventQueue[0];
            EventQueue.RemoveAt(0);
            first.Execute(GridPointer, Context);
            var playerCoord = GridPointer.GetPlayerCoord();
            var x = playerCoord.X;
            var y = playerCoord.Y;

            var playerObj = GridPointer.GetObject(playerCoord) as PlayerObject;
            Console.WriteLine($"Coordinate: {x}, {y}");
            Console.WriteLine($"Direction: {playerObj.Direction}");
        }
    }
}
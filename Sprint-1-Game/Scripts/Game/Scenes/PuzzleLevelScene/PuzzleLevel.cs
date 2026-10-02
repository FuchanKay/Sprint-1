using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class PuzzleLevel(ObjectFactory objectFactory, TileFactory tileFactory)
{
    private List<ILevelEvent> EventQueue = [];
    private LevelContext Context = new();
    private GridPointer GridPointer = new();
    private readonly int MaxEventsQueued = 3; 
    private readonly int LevelWidth = 10;
    private readonly int LevelHeight = 10;
    public bool IsIdle = true;

    public void Init()
    {
        GridPointer.Init();
        for (int i = 0; i < LevelWidth * LevelHeight; i++)
        {
            var x = i % LevelWidth;
            var y = i / LevelHeight;
            var coord = new Vector2(x, y);

            var emptyGrid = new Grid
            {
                Object = new EmptyObject(),
                Tile = new EmptyTile()
            };

            GridPointer.SetGrid(coord, emptyGrid);
        }

        GridPointer.SetObject(new Vector2(3, 3), new RockObject());
        GridPointer.SetObject(new Vector2(4, 4), new PlayerObject());
    }

    public void Draw(SpriteBatch sb)
    {
        for (int i = 0; i < LevelWidth * LevelHeight; i++)
        {
            var x = i % LevelWidth;
            var y = i / LevelHeight;
            var coord = new Vector2(x, y);




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
        if (IsIdle && EventQueue.Count > 0)
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
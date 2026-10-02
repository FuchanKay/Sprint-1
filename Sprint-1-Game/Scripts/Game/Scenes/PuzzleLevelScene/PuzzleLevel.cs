using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class PuzzleLevel
{
    private List<ILevelEvent> EventQueue = [];
    private LevelContext Context = new();
    private GridPointer GridPointer = new();
    private readonly int MaxEventsQueued = 3; 
    private readonly int LevelWidth = 10;
    private readonly int LevelHeight = 10;
    public bool IsIdle = true;

    public PuzzleLevel()
    {
        
    }

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

    public void Draw()
    {

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
        if (IsIdle)
        {
            var first = EventQueue[0];
            EventQueue.RemoveAt(0);
            first.Execute(GridPointer, Context);
        }
    }
}
using System.Collections.Generic;
using System.Net;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class PuzzleLevel(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly List<ILevelEvent> EventQueue = [];
    //TODO: Some constants regarding the width and height of each grid, the size of hte level, etc. probably should be moved somewhere else idk where
    private readonly LevelContext Context = new()
    {
        LevelWidth = 10,
        LevelHeight = 10,
        GridSpriteWidthPx = 256,
        GridSpriteHeightPx = 256,
        GridScale = 0.25f,
        PuzzleOffsetX = 400,
        PuzzleOffsetY = 50,
        IsIdle = true
    };
    private readonly GridPointer GridPointer = new();
    private readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    public bool IsIdle = true;
    private ILevelEvent CurrentEvent = new MovePlayerEvent(Directions.East, objectFactory);

    public void Init()
    {
        //Temporary level initialization code
        GridPointer.Init();
        CreateLevel();
        SpriteGrid.Init(GridPointer, Context);
    }

    public void Update(int dtMs)
    {
        SpriteGrid.Update(dtMs);
        Context.IsIdle = SpriteGrid.IsIdle;
    }

    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
    }

    public void EnqueueEvent(ILevelEvent levelEvent)
    {
        if (EventQueue.Count < MaxEventsQueued)
        {
            EventQueue.Add(levelEvent);
        }
    }

    public bool TryExecuteEvent()
    {
        if (CurrentEvent == null) return false;
        if (!Context.IsIdle) return false;

        var CurrentEventIsFinished = CurrentEvent.SubEventStack.Count() == 0;
        if (CurrentEventIsFinished && EventQueue.Count > 0)
        {
            CurrentEvent = EventQueue[0];
            EventQueue.RemoveAt(0);

            CurrentEvent.Execute(SpriteGrid, GridPointer, Context);
            return true;
        }

        if (!CurrentEventIsFinished)
        {
            var subEvent = CurrentEvent.SubEventStack.Pop();
            subEvent.Execute(SpriteGrid, GridPointer, Context);
        }

        return false;
    }

    private void CreateLevel()
    {
        for (int i = 0; i < Context.LevelWidth * Context.LevelHeight; i++)
        {
            var x = i % Context.LevelWidth;
            var y = i / Context.LevelHeight;
            var coord = new Coordinate(x, y);

            var defaultGrid = new Grid
            {
                Object = objectFactory.CreateEmpty(),
                Tile = tileFactory.CreateDirtTile()
            };

            GridPointer.SetGrid(coord, defaultGrid);
        }

        CreateWallBorders();

        GridPointer.SetObject(new Coordinate(7, 5), objectFactory.CreateSkeleton());
        GridPointer.SetObject(new Coordinate(7, 2), objectFactory.CreateTimedBomb());
        GridPointer.SetObject(new Coordinate(6, 2), objectFactory.CreateDefaultRock());
        GridPointer.SetObject(new Coordinate(2, 2), objectFactory.CreatePlayer());

        GridPointer.SetTile(new Coordinate(2, 2), tileFactory.CreateWaterTile());
        GridPointer.SetTile(new Coordinate(2, 3), tileFactory.CreateWaterTile());
        GridPointer.SetTile(new Coordinate(3, 2), tileFactory.CreateWaterTile());
        GridPointer.SetTile(new Coordinate(3, 3), tileFactory.CreateWaterTile());
        GridPointer.SetTile(new Coordinate(4, 3), tileFactory.CreateLavaTile());

        GridPointer.SetTile(new Coordinate(5, 5), tileFactory.CreateBrickTile());
        GridPointer.SetTile(new Coordinate(6, 5), tileFactory.CreateBrickTile());
        GridPointer.SetTile(new Coordinate(5, 6), tileFactory.CreateBrickTile());
        GridPointer.SetTile(new Coordinate(6, 6), tileFactory.CreatePitTile());
        GridPointer.SetTile(new Coordinate(6, 7), tileFactory.CreateMossyBrickTile());
        GridPointer.SetTile(new Coordinate(7, 6), tileFactory.CreateMossyBrickTile());
        GridPointer.SetTile(new Coordinate(2, 2), tileFactory.CreateWaterTile());
        GridPointer.SetTile(new Coordinate(7, 7), tileFactory.CreateGrassTile());

        GridPointer.SetTile(new Coordinate(2, 7), tileFactory.CreateBugTile());
        GridPointer.SetTile(new Coordinate(3, 7), tileFactory.CreateCCBugTile());
    }

    private void CreateWallBorders()
    {
        for (int x = 0; x < Context.LevelWidth; x++)
        {
            var topOfLevel = new Coordinate(x, 0);
            var bottomOfLevel = new Coordinate(x, Context.LevelHeight - 1);

            GridPointer.SetObject(topOfLevel, objectFactory.CreateWall());
            GridPointer.SetObject(bottomOfLevel, objectFactory.CreateWall());
        }
        for (int y = 0; y < Context.LevelWidth; y++)
        {
            var leftOfLevel = new Coordinate(0, y);
            var rightOfLevel = new Coordinate(Context.LevelWidth - 1, y);

            GridPointer.SetObject(leftOfLevel, objectFactory.CreateWall());
            GridPointer.SetObject(rightOfLevel, objectFactory.CreateWall());
        }
    }
}
using System.Collections.Generic;
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
        GridWidthPx = 256,
        GridHeightPx = 256,
        GridScale = 0.15f,
        PuzzleOffsetX = 400,
        PuzzleOffsetY = 50,
        IsIdle = true
    };
    private readonly GridPointer GridPointer = new();
    private readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    public bool IsIdle = true;

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
        var shouldExecute = Context.IsIdle && EventQueue.Count > 0;
        if (shouldExecute)
        {
            var first = EventQueue[0];
            EventQueue.RemoveAt(0);
            first.Execute(SpriteGrid, GridPointer, Context);
        }
        return shouldExecute;
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
                Tile = tileFactory.CreateBrickTile()
            };

            GridPointer.SetGrid(coord, defaultGrid);
        }

        CreateWallBorders();

        GridPointer.SetObject(new Coordinate(7, 5), objectFactory.CreateSkeleton());
        GridPointer.SetObject(new Coordinate(7, 2), objectFactory.CreateBomb());
        GridPointer.SetObject(new Coordinate(2, 2), objectFactory.CreatePlayer());
        GridPointer.SetTile(new Coordinate(5, 5), tileFactory.CreateGrassTile());
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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
    private readonly int MaxEventsQueued = 3;
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
        //TODO: An event should only execute once all animations and movements are finished from the previous event. IsIdle should keep track of that
        var shouldExecute = Context.IsIdle && EventQueue.Count > 0;
        if (shouldExecute)
        {
            var first = EventQueue[0];
            EventQueue.RemoveAt(0);
            first.Execute(SpriteGrid, GridPointer, Context);

            //TODO: THE SPRITE GRID SHOULD NOT INITIATE EVERY SINGLE TIME AN EVENT HAPPENS! THIS *MUST* BE CHANGED
            SpriteGrid.Init(GridPointer, Context);
        }
        return shouldExecute;
    }

    private void CreateLevel()
    {
        for (int i = 0; i < Context.LevelWidth * Context.LevelHeight; i++)
        {
            var x = i % Context.LevelWidth;
            var y = i / Context.LevelHeight;
            var coord = new Vector2(x, y);

            var defaultGrid = new Grid
            {
                Object = objectFactory.CreateEmpty(),
                Tile = tileFactory.CreateBrickTile()
            };

            GridPointer.SetGrid(coord, defaultGrid);
        }

        CreateWallBorders();

        GridPointer.SetObject(new Vector2(7, 2), objectFactory.CreateBomb());
        GridPointer.SetObject(new Vector2(2, 2), objectFactory.CreatePlayer());
    }

    private void CreateWallBorders()
    {
        for (int x = 0; x < Context.LevelWidth; x++)
        {
            var topCoord = new Vector2(x, 0);
            var bottomCoord = new Vector2(x, Context.LevelHeight - 1);

            GridPointer.SetObject(topCoord, objectFactory.CreateWall());
            GridPointer.SetObject(bottomCoord, objectFactory.CreateWall());
        }
        for (int y = 0; y < Context.LevelWidth; y++)
        {
            var leftCoord = new Vector2(0, y);
            var rightCoord = new Vector2(Context.LevelWidth - 1, y);

            GridPointer.SetObject(leftCoord, objectFactory.CreateWall());
            GridPointer.SetObject(rightCoord, objectFactory.CreateWall());
        }
    }
}
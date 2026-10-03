using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PuzzleLevel(ObjectFactory objectFactory, TileFactory tileFactory, ITextureAtlas textureAtlas)
{
    private readonly List<ILevelEvent> EventQueue = [];
    private readonly LevelContext Context = new()
    {
        LevelWidth = 10,
        LevelHeight = 10,
        GridWidthPx = 256,
        GridHeightPx = 256,
        GridScale = 0.2f,
        PuzzleOffsetX = 100,
        PuzzleOffsetY = 100,
        IsIdle = true
    };
    private readonly GridPointer GridPointer = new();
    private readonly SpriteGrid SpriteGrid = new(textureAtlas);
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

            var defaultGrid = new Grid
            {
                Object = objectFactory.CreateEmpty(),
                Tile = tileFactory.CreateBrickTile()
            };

            GridPointer.SetGrid(coord, defaultGrid);
        }

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
        GridPointer.SetObject(new Vector2(5, 5), objectFactory.CreateRock());
        GridPointer.SetObject(new Vector2(6, 6), objectFactory.CreatePlayer());

        SpriteGrid.Init(GridPointer, Context);
    }

    public void Update(int dtMs)
    {
        
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
        }
    }
}
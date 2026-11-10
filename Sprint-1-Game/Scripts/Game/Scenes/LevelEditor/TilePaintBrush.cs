using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class TilePaintBrush(IInputManager buttonInput, IInputManager mouseInput, SpriteGrid spriteGrid, LevelContext context, TileSpriteFactory tileSpriteFactory, List<GridPointer> undoHistory) : IBrush
{
    private static ITile[] TilePalette =
    {
        TileFactory.CreateBrickTile(),
        TileFactory.CreateBugTile(),
        TileFactory.CreateCCBugTile(),
        TileFactory.CreateDirtTile(),
        TileFactory.CreateGrassTile(),
        TileFactory.CreateLavaTile(),
        TileFactory.CreateMossyBrickTile(),
        TileFactory.CreatePitBoulderTile(),
        TileFactory.CreatePitPumpkinTile(),
        TileFactory.CreatePitTile(),
        TileFactory.CreatePumpkinSeedTile(),
        TileFactory.CreateVineSeedTile(),
        TileFactory.CreateVineStartTile(),
        TileFactory.CreateVineTile(),
        TileFactory.CreateWaterBoulderTile(),
        TileFactory.CreateWaterPumpkinTile(),
        TileFactory.CreateWaterTile()
    };
    private ITile CurrentTile;
    private ISprite CurrentTileSprite;
    private int TileIndex;
    public BrushTypes Type => BrushTypes.TilePaint;
    public GridPointer Grid;

    public void Init(GridPointer grid)
    {
        Grid = grid;
        CurrentTile = TilePalette[TileIndex].Clone();
        CurrentTileSprite = tileSpriteFactory.CreateTileSprite(CurrentTile, new Vector2(0, 0), context);
    }

    public void Update(int dtMs)
    {
        if (buttonInput.IsPressed(InputNames.RotatePaintBrush) && CurrentTile.IsRotatable)
        {
            CurrentTile.Direction = Utilities.RotateClockwise(CurrentTile.Direction);
            CurrentTileSprite.Rotation = Utilities.ToRadians(CurrentTile.Direction);
        }
        if (mouseInput.IsHeld(InputNames.Select) && !IsOutOfBounds())
        {
            var coord = MouseCoordinate();
            var tile = Grid.GetTile(coord);
            var shouldChange = false;
            if (tile.Id == TileIds.Floor && CurrentTile.Id == TileIds.Floor)
            {
                var tileAsFloor = (FloorTile)tile;
                var currentTileAsFloor = (FloorTile)CurrentTile;
                shouldChange = tileAsFloor.RegionName != currentTileAsFloor.RegionName;
            }
            if (!shouldChange) shouldChange = tile.Id != CurrentTile.Id || tile.Direction != CurrentTile.Direction;
            if (shouldChange)
            {
                CurrentTile = CurrentTile.Clone();
                undoHistory.Add(Grid.Clone());
                Grid.SetTile(coord, CurrentTile);
                spriteGrid.SetNewTileSprite(coord, CurrentTile);
            }
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (!IsOutOfBounds())
        {
            var mouseCoord = MouseCoordinate();
            var literalPosition = SpriteGrid.CalculateLiteralPos(context, mouseCoord);
            CurrentTileSprite.CurrentPosition = literalPosition;
        }
        else
        {
            CurrentTileSprite.CurrentPosition = new Vector2(mouseInput.MousePositionX, mouseInput.MousePositionY);
        }
        CurrentTileSprite.Draw(sb);
    }

    private bool IsOutOfBounds()
    {
        var leftmostPoint = context.PuzzleOffsetX;
        var rightmostPoint = (int)(context.LevelWidth * context.RawSpriteWidthPx * context.GridScale + context.PuzzleOffsetX);
        var topMostPoint = context.PuzzleOffsetY;
        var bottomMostPoint = (int)(context.LevelWidth * context.RawSpriteHeightPx * context.GridScale + context.PuzzleOffsetY);

        var mouseX = mouseInput.MousePositionX;
        var mouseY = mouseInput.MousePositionY;

        var outOfBounds = mouseX < leftmostPoint || mouseX > rightmostPoint || mouseY < topMostPoint || mouseY > bottomMostPoint;
        return outOfBounds;
    }

    private Coordinate MouseCoordinate()
    {
        var mouseX = mouseInput.MousePositionX;
        var mouseY = mouseInput.MousePositionY;

        var leftmostPoint = context.PuzzleOffsetX;
        var topMostPoint = context.PuzzleOffsetY;

        var gridWidth = (int)(context.RawSpriteWidthPx * context.GridScale);
        var gridHeight = (int)(context.RawSpriteHeightPx * context.GridScale);

        var x = (mouseX - leftmostPoint) / gridWidth;
        var y = (mouseY - topMostPoint) / gridHeight;

        if (x >= context.LevelWidth) x = context.LevelWidth - 1;
        if (y >= context.LevelHeight) y = context.LevelHeight - 1;
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        return new Coordinate(x, y);
    }

    public void CycleRight()
    {
        TileIndex++;
        if (TileIndex >= TilePalette.Length) TileIndex = 0;
        CurrentTile = TilePalette[TileIndex].Clone();
        CurrentTileSprite = tileSpriteFactory.CreateTileSprite(CurrentTile, new Vector2(0, 0), context);
    }

    public void CycleLeft()
    {
        TileIndex--;
        if (TileIndex < 0) TileIndex = TilePalette.Length - 1;
        CurrentTile = TilePalette[TileIndex].Clone();
        CurrentTileSprite = tileSpriteFactory.CreateTileSprite(CurrentTile, new Vector2(0, 0), context);
    }
}
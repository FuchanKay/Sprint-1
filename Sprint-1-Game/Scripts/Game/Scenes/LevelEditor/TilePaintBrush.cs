using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class TilePaintBrush(IInputManager buttonInput, IInputManager mouseInput, SpriteGrid spriteGrid, LevelContext context, TileSpriteFactory tileSpriteFactory, List<GridPointer> undoHistory, List<GridPointer> redoHistory) : IBrush
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
        if (mouseInput.IsHeld(InputNames.Select)
            && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, context, out var coord))
        {
            var tile = Grid.GetTile(coord);
            var shouldChange = tile.Id != CurrentTile.Id || tile.Direction != CurrentTile.Direction || tile.RegionName != CurrentTile.RegionName;
            if (shouldChange)
            {
                redoHistory.Clear();
                undoHistory.Add(Grid.Clone());
                var placedTile = CurrentTile.Clone();
                Grid.SetTile(coord, placedTile);
                spriteGrid.SetNewTileSprite(coord, placedTile);
            }
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, context, out var mouseCoord))
        {
            var literalPosition = SpriteGrid.CalculateLiteralPos(context, mouseCoord);
            CurrentTileSprite.CurrentPosition = literalPosition;
        }
        else
        {
            CurrentTileSprite.CurrentPosition = new Vector2(mouseInput.MousePositionX, mouseInput.MousePositionY);
        }
        CurrentTileSprite.Draw(sb);
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
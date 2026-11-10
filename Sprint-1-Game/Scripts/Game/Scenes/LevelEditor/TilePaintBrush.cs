using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class TilePaintBrush(IInputManager buttonInput, IInputManager mouseInput, GridPointer grid, SpriteGrid spriteGrid, LevelContext context, TileSpriteFactory tileSpriteFactory) : IBrush
{
    private ITile CurrentTile;
    private ISprite CurrentTileSprite;
    public BrushTypes Type => BrushTypes.TilePaint;

    public void Init()
    {
        CurrentTile = TileFactory.CreateBrickTile();
        CurrentTileSprite = tileSpriteFactory.CreateTileSprite(CurrentTile, new Vector2(0, 0), context);
    }

    public void Update(int dtMs)
    {
        if (mouseInput.IsHeld(InputNames.Select) && !IsOutOfBounds())
        {
            var coord = MouseCoordinate();
            grid.SetTile(coord, CurrentTile);
            spriteGrid.SetNewTileSprite(coord, CurrentTile);
            CurrentTile = CurrentTile.Clone();
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
        var rightmostPoint = (int) (context.LevelWidth * context.RawSpriteWidthPx * context.GridScale + context.PuzzleOffsetX);
        var topMostPoint = context.PuzzleOffsetY;
        var bottomMostPoint = (int) (context.LevelWidth * context.RawSpriteHeightPx * context.GridScale + context.PuzzleOffsetY);

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

        var gridWidth = (int) (context.RawSpriteWidthPx * context.GridScale);
        var gridHeight = (int) (context.RawSpriteHeightPx * context.GridScale);

        var x = (mouseX - leftmostPoint) / gridWidth;
        var y = (mouseY - topMostPoint) / gridHeight;
        
        if (x >= context.LevelWidth) x = context.LevelWidth - 1;
        if (y >= context.LevelHeight) y = context.LevelHeight - 1;
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        return new Coordinate(x, y);
    }
}
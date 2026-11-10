using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectPaintBrush(IInputManager buttonInput, IInputManager mouseInput, GridPointer grid, SpriteGrid spriteGrid, LevelContext context, ObjectSpriteFactory objectSpriteFactory) : IBrush
{
    private IObject CurrentObject;
    private ISprite CurrentObjectSprite;

    public BrushTypes Type => BrushTypes.ObjectPaint;

    public void Init()
    {
        CurrentObject = ObjectFactory.CreateCornerWall(Directions.North);
        CurrentObjectSprite = objectSpriteFactory.CreateObjectSprite(CurrentObject, new Vector2(0, 0), context);
    }

    public void Update(int dtMs)
    {
        if (buttonInput.IsPressed(InputNames.RotateObject) && CurrentObject.IsRotatable)
        {
            CurrentObject.Direction = Utilities.RotateClockwise(CurrentObject.Direction);
            CurrentObjectSprite.Rotation = Utilities.ToRadians(CurrentObject.Direction);
        }
        if (mouseInput.IsHeld(InputNames.Select) && !IsOutOfBounds())
        {
            var coord = MouseCoordinate();
            grid.SetObject(coord, CurrentObject);
            spriteGrid.SetNewObjectSprite(coord, CurrentObject);
            CurrentObject = CurrentObject.Clone();
        }
    }
    
    public void Draw(SpriteBatch sb)
    {
        if (!IsOutOfBounds())
        {
            var mouseCoord = MouseCoordinate();
            var literalPosition = SpriteGrid.CalculateLiteralPos(context, mouseCoord);
            CurrentObjectSprite.CurrentPosition = literalPosition;
        }
        else
        {
            CurrentObjectSprite.CurrentPosition = new Vector2(mouseInput.MousePositionX, mouseInput.MousePositionY);
        }
        CurrentObjectSprite.Draw(sb);
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
        
        //the coordinate calculation can sometimes jankily evaluate to an index that is out of bounds
        if (x >= context.LevelWidth) x = context.LevelWidth - 1;
        if (y >= context.LevelHeight) y = context.LevelHeight - 1;
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        return new Coordinate(x, y);
    }
}
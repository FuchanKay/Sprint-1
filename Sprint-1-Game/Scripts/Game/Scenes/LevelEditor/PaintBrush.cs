using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PaintBrush(IInputManager buttonInput, IInputManager mouseInput)
{
    private LevelContext Context;
    private GridPointer Grid;
    private SpriteGrid SpriteGrid; 
    private IObject CurrentObject;
    private ISprite CurrentObjectSprite;
    public void Init(GridPointer grid, SpriteGrid spriteGrid, ObjectSpriteFactory objectSpriteFactory, LevelContext context)
    {
        Context = context;
        Grid = grid;
        SpriteGrid = spriteGrid;
        CurrentObject = ObjectFactory.CreatePumpkin();
        CurrentObjectSprite = objectSpriteFactory.CreateObjectSprite(CurrentObject, new Vector2(0, 0), context);
    }

    public void Update(int dtMs)
    {
        if (mouseInput.IsHeld(InputNames.Select) && !IsOutOfBounds())
        {
            var coord = MouseCoordinate();
            Grid.SetObject(coord, CurrentObject);
            SpriteGrid.SetNewObjectSprite(coord, CurrentObject);
            SpriteGrid.GetObjectSprite(coord);
            CurrentObject = CurrentObject.Clone();
        }
        if (mouseInput.IsHeld(InputNames.Erase) && !IsOutOfBounds())
        {
            var coord = MouseCoordinate();
            var empty = ObjectFactory.CreateEmpty();
            Grid.SetObject(coord, empty);
            SpriteGrid.RemoveObjectSprite(coord);
        }
    }
    
    public void Draw(SpriteBatch sb)
    {
        if (!IsOutOfBounds())
        {
            var mouseCoord = MouseCoordinate();
            var literalPosition = SpriteGrid.CalculateLiteralPos(Context, mouseCoord);
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
        var leftmostPoint = Context.PuzzleOffsetX;
        var rightmostPoint = (int) (Context.LevelWidth * Context.RawSpriteWidthPx * Context.GridScale + Context.PuzzleOffsetX);
        var topMostPoint = Context.PuzzleOffsetY;
        var bottomMostPoint = (int) (Context.LevelWidth * Context.RawSpriteHeightPx * Context.GridScale + Context.PuzzleOffsetY);

        var mouseX = mouseInput.MousePositionX;
        var mouseY = mouseInput.MousePositionY;

        var outOfBounds = mouseX < leftmostPoint || mouseX > rightmostPoint || mouseY < topMostPoint || mouseY > bottomMostPoint;
        return outOfBounds;
    }
    
    public Coordinate MouseCoordinate()
    {
        var mouseX = mouseInput.MousePositionX;
        var mouseY = mouseInput.MousePositionY;
        
        var leftmostPoint = Context.PuzzleOffsetX;
        var topMostPoint = Context.PuzzleOffsetY;

        var gridWidth = (int) (Context.RawSpriteWidthPx * Context.GridScale);
        var gridHeight = (int) (Context.RawSpriteHeightPx * Context.GridScale);

        var x = (mouseX - leftmostPoint) / gridWidth;
        var y = (mouseY - topMostPoint) / gridHeight;
        
        //the coordinate calculation can sometimes jankily evaluate to an index that is out of bounds
        if (x >= Context.LevelWidth) x = Context.LevelWidth - 1;
        if (y >= Context.LevelHeight) y = Context.LevelHeight - 1;

        return new Coordinate(x, y);
    }
}
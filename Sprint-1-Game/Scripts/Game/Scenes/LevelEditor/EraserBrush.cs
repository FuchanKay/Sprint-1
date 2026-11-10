using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class EraserBrush(IInputManager mouseInput, SpriteGrid spriteGrid, LevelContext context, List<GridPointer> undoHistory) : IBrush
{
    public GridPointer Grid;
    public BrushTypes Type => BrushTypes.Eraser;
    public void Init(GridPointer grid)
    {
        Grid = grid;
    }

    public void Update(int dtMs)
    {
        if (mouseInput.IsHeld(InputNames.Select)
            && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, context, out var coord))
        {
            var obj = Grid.GetObject(coord);
            if (obj.Id != ObjectIds.Empty)
            {
                var empty = ObjectFactory.CreateEmpty();
                undoHistory.Add(Grid.Clone());
                Grid.SetObject(coord, empty);
                spriteGrid.RemoveObjectSprite(coord);
            }
        }
    }

    public void Draw(SpriteBatch sb)
    {

    }

    public void CycleRight()
    {

    }

    public void CycleLeft()
    {

    }
}
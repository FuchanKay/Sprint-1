using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectPaintBrush(IInputManager buttonInput, IInputManager mouseInput, SpriteGrid spriteGrid, LevelContext context, ObjectSpriteFactory objectSpriteFactory, List<GridPointer> undoHistory) : IBrush
{
    private static readonly IObject[] ObjectPalette = [
        ObjectFactory.CreateBat(),
        ObjectFactory.CreateBlueLizard(Directions.North),
        ObjectFactory.CreateBomb(),
        ObjectFactory.CreateClosedDoor(),
        ObjectFactory.CreateCornerWall(Directions.North),
        ObjectFactory.CreateDefaultRock(),
        ObjectFactory.CreateFrog(),
        ObjectFactory.CreateInvertedCornerWall(Directions.North),
        ObjectFactory.CreateKey(),
        ObjectFactory.CreateOpenDoor(),
        ObjectFactory.CreatePlayer(),
        ObjectFactory.CreatePumpkin(),
        ObjectFactory.CreatePumpkinSeed(),
        ObjectFactory.CreateRedLizard(Directions.North),
        ObjectFactory.CreateSkeleton(),
        ObjectFactory.CreateStandaloneWall(Directions.North),
        ObjectFactory.CreateStandaloneWallConnector(Directions.North),
        ObjectFactory.CreateStandaloneWallCorner(Directions.North),
        ObjectFactory.CreateTimedBomb(),
        ObjectFactory.CreateTopOfWall(Directions.North),
        ObjectFactory.CreateVineSeed(Directions.North),
        ObjectFactory.CreateWall(Directions.North),
        ObjectFactory.CreateWarlock(),
        ObjectFactory.CreateZombie()
    ];
    private IObject CurrentObject;
    private ISprite CurrentObjectSprite;
    private int ObjectIndex = 0;
    private GridPointer Grid;
    public BrushTypes Type => BrushTypes.ObjectPaint;

    public void Init(GridPointer grid)
    {
        Grid = grid;
        CurrentObject = ObjectPalette[ObjectIndex].Clone();
        CurrentObjectSprite = objectSpriteFactory.CreateObjectSprite(CurrentObject, new Vector2(0, 0), context);
    }

    public void Update(int dtMs)
    {
        if (buttonInput.IsPressed(InputNames.RotatePaintBrush) && CurrentObject.IsRotatable)
        {
            CurrentObject.Direction = Utilities.RotateClockwise(CurrentObject.Direction);
            CurrentObjectSprite.Rotation = Utilities.ToRadians(CurrentObject.Direction);
        }
        if (mouseInput.IsHeld(InputNames.Select)
            && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, context, out var coord))
        {
            var obj = Grid.GetObject(coord);
            if (obj.Id != CurrentObject.Id || obj.Direction != CurrentObject.Direction)
            {
                CurrentObject = CurrentObject.Clone();
                undoHistory.Add(Grid.Clone());
                Grid.SetObject(coord, CurrentObject);
                spriteGrid.SetNewObjectSprite(coord, CurrentObject);
            }
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, context, out var mouseCoord))
        {
            var literalPosition = SpriteGrid.CalculateLiteralPos(context, mouseCoord);
            CurrentObjectSprite.CurrentPosition = literalPosition;
        }
        else
        {
            CurrentObjectSprite.CurrentPosition = new Vector2(mouseInput.MousePositionX, mouseInput.MousePositionY);
        }
        CurrentObjectSprite.Draw(sb);
    }

    public void CycleRight()
    {
        ObjectIndex++;
        if (ObjectIndex >= ObjectPalette.Length) ObjectIndex = 0;
        CurrentObject = ObjectPalette[ObjectIndex];
        CurrentObjectSprite = objectSpriteFactory.CreateObjectSprite(CurrentObject, new Vector2(0, 0), context);
    }

    public void CycleLeft()
    {
        ObjectIndex--;
        if (ObjectIndex < 0) ObjectIndex = ObjectPalette.Length - 1;
        CurrentObject = ObjectPalette[ObjectIndex];
        CurrentObjectSprite = objectSpriteFactory.CreateObjectSprite(CurrentObject, new Vector2(0, 0), context);
    }
}
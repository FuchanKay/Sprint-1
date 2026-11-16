using Scripts.GameComponents;

namespace Scripts.Game;

internal static class LevelEditorMouseCoordinates
{
    public static bool TryGetGridCoordinate(IInputManager mouseInput, LevelContext context, out Coordinate coordinate)
    {
        coordinate = Coordinate.Zero;

        var gridWidth = context.GridWidth;
        var gridHeight = context.GridHeight;
        if (context.LevelWidth <= 0 || context.LevelHeight <= 0 || gridWidth <= 0 || gridHeight <= 0)
        {
            return false;
        }

        var relativeX = mouseInput.MousePositionX - context.PuzzleOffsetX;
        var relativeY = mouseInput.MousePositionY - context.PuzzleOffsetY;
        if (relativeX < 0 || relativeY < 0
            || relativeX >= context.LevelWidth * gridWidth
            || relativeY >= context.LevelHeight * gridHeight)
        {
            return false;
        }

        coordinate = new Coordinate(relativeX / gridWidth, relativeY / gridHeight);
        return true;
    }
}

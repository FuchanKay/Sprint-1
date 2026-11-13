using System;
using System.IO;

namespace Scripts.Game;

public class LevelFileWriter()
{
    private const string Delimiter = ",";
    private const string DirectionDelimiter = ".";
    public static void WriteLevelFile(GridPointer gridPointer, LevelContext levelContext)
    {
        string[] levelFile = new string[levelContext.LevelHeight * 2 + 2];

        WriteHeader(levelFile, levelContext);
        WriteGrids(levelFile, gridPointer, levelContext);
    }

    private static void WriteHeader(string[] levelFile, LevelContext levelContext)
    {
        string header = $"{levelContext.LevelName}{Delimiter}{levelContext.LevelWidth}{Delimiter}{levelContext.LevelHeight}";
        for (int i = 0; i < levelContext.LevelWidth - 3; i++)
        {
            header += Delimiter;
        }
        levelFile[0] = header;
    }
    private static void WriteGrids(string[] levelFile, GridPointer gridPointer, LevelContext levelContext)
    {
        int tileGridRowIdx = 1;
        int objectGridRowIdx = 1 + levelContext.LevelHeight + 1;

        for (int y = 0; y < levelContext.LevelHeight; y++)
        {
            string tileLine = "";
            string objectLine = "";
            for (int x = 0; x < levelContext.LevelWidth; x++)
            {
                var coord = new Coordinate(x, y);
                var grid = gridPointer.GetGrid(coord);
                tileLine += WriteTile(grid);
                objectLine += WriteObject(grid);
                if (x < levelContext.LevelWidth - 1)
                {
                    tileLine += Delimiter;
                    objectLine += Delimiter;
                }
                
            }
            levelFile[tileGridRowIdx + y] = tileLine;
            levelFile[objectGridRowIdx + y] = objectLine;
        }

        levelFile[tileGridRowIdx + levelContext.LevelHeight] = WriteSpacerRow(levelContext);
    }

    private static string WriteSpacerRow(LevelContext levelContext)
    {
        string spacerRow = "";
        for (int i = 0; i < levelContext.LevelWidth; i++)
        {
            spacerRow += Delimiter;
        }
        return spacerRow;
    }

    private static string WriteTile(Grid grid)
    {
        return grid.Tile.Id.ToString() + DirectionDelimiter + grid.Tile.Direction;
    }

    private static string WriteObject(Grid grid)
    {
        return grid.Object.Id.ToString() + DirectionDelimiter + grid.Object.Direction;
    }
}

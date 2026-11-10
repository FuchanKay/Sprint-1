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
        WriteTileGrid(levelFile, gridPointer, levelContext);
        WriteObjectGrid(levelFile, gridPointer, levelContext);
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
    private static void WriteTileGrid(string[] levelFile, GridPointer gridPointer, LevelContext levelContext)
    {
        
    }

    private static void WriteObjectGrid(string[] levelFile, GridPointer gridPointer, LevelContext levelContext)
    {
        
    }
}

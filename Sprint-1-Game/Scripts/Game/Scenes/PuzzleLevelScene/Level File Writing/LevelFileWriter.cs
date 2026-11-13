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
        
        string levelFilePath = "Content/Levels/" + levelContext.LevelName + ".csv";
        File.WriteAllLines(levelFilePath, levelFile);
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
        string tileString;
        string TileID = grid.Tile.Id.ToString();
        string TileDirection = ((int)grid.Tile.Direction).ToString();

        if (TileID == "Floor")
        {
            var floorTile = (FloorTile)grid.Tile;
            tileString = floorTile.RegionName.Replace(" ", string.Empty);
        }
        else
        {
            tileString = TileID + DirectionDelimiter + TileDirection;
        }

        return tileString;
    }

    private static string WriteObject(Grid grid)
    {
        string objectString;
        string ObjectID = grid.Object.Id.ToString();
        string ObjectDirection = ((int)grid.Object.Direction).ToString();

        if (ObjectID == "Empty")
        {
            objectString = "_";
        } 
        else if (ObjectID == "Wall")
        {
            var wallObject = (WallObject)grid.Object;
            objectString = wallObject.RegionName.Replace(" ", string.Empty) + DirectionDelimiter + ObjectDirection;
        }
        else if (ObjectID == "Rock")
        {
            var rockObject = (RockObject)grid.Object;
            objectString = rockObject.RegionName.Replace(" ", string.Empty) + DirectionDelimiter + ObjectDirection;
        }
        else if (ObjectID == "Door")
        {
            var doorObject = (DoorObject)grid.Object;
            string doorOpenStatus = doorObject.IsOpen ? "OpenDoor" : "ClosedDoor";
            objectString = doorOpenStatus + DirectionDelimiter + ObjectDirection;
        }
        else
        {
            objectString = ObjectID + DirectionDelimiter + ObjectDirection;
        }
        

        return objectString;
    }
}

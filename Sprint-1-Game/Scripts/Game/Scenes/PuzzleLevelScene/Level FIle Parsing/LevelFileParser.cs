using System;
using System.IO;

namespace Scripts.Game;

public class LevelFileParser( )
{
    private readonly string Delimiter = ",";
    private readonly string ObjectDelimiter = ".";
    private readonly int NameIdx = 0;
    private readonly int WidthIdx = 1;
    private readonly int HeightIdx = 2;
    private readonly int SpacerRow = 1;
    public static string[] ParseLevelFile(string levelFilePath)
    {
        return File.ReadAllLines(levelFilePath);
    }

    public string GetLevelName(string[] levelFile)
    {
        string[] headerParts = levelFile[0].Split(Delimiter);
        return headerParts[NameIdx];
    }

    public int GetLevelWidth(string[] levelFile)
    {
        string[] headerParts = levelFile[0].Split(Delimiter);
        return int.Parse(headerParts[WidthIdx]);
    }

    public int GetLevelHeight(string[] levelFile)
    {
        string[] headerParts = levelFile[0].Split(Delimiter);
        return int.Parse(headerParts[HeightIdx]);
    }

    public GridPointer CreateGrids(string[] levelFile)
    {
        var gridPointer = new GridPointer();
        gridPointer.Init();

        int levelHeight = GetLevelHeight(levelFile);
        int levelWidth = GetLevelWidth(levelFile);

        int tileGridIdx = SpacerRow;
        int objectGridIdx = SpacerRow * 2 + levelHeight;

        for (int y = 0; y < levelHeight; y++)
        {
            string tileLine = levelFile[tileGridIdx + y];
            string objectLine = levelFile[objectGridIdx + y];
            string[] tileValues = tileLine.Split(Delimiter);
            string[] objectValues = objectLine.Split(Delimiter);

            for (int x = 0; x < levelWidth; x++)
            {
                string tileValue = tileValues[x];
                string objectValue = objectValues[x];
                ITile tile = ProcessTile(tileValue, x, y);
                IObject obj = ProcessObject(objectValue, x, y);
                Grid grid = new()
                {
                    Tile = tile,
                    Object = obj
                };
                gridPointer.SetGrid(new Coordinate(x, y), grid);
            }
        }
        return gridPointer;
    }

    private ITile ProcessTile(string tileValue, int x, int y)
    {
        ITile tile = tileValue switch
        {
            "_" => TileFactory.CreateEmptyTile(),
            "Dirt" => TileFactory.CreateDirtTile(),
            "Grass" => TileFactory.CreateGrassTile(),
            "Brick" => TileFactory.CreateBrickTile(),
            "MossyBrick" => TileFactory.CreateMossyBrickTile(),
            "Pit" => TileFactory.CreatePitTile(),
            "Lava" => TileFactory.CreateLavaTile(),
            "Water" => TileFactory.CreateWaterTile(),
            "Bug" => TileFactory.CreateBugTile(),
            "CCBug" => TileFactory.CreateCCBugTile(),
            _ => throw new Exception($"Unknown tile value: {tileValue} at position ({x}, {y})")
        };
        return tile;
    }

    private IObject ProcessObject(string objectValue, int x, int y)
    {
        string[] objectParts = objectValue.Split(ObjectDelimiter);
        string objectType = objectParts[0];

        IObject obj = objectType switch
        {
            "_" => ObjectFactory.CreateEmpty(),
            "Player" => ObjectFactory.CreatePlayer(),
            "Rock" => ObjectFactory.CreateDefaultRock(),
            "Pumpkin" => ObjectFactory.CreatePumpkin(),
            "Wall" => ObjectFactory.CreateWall(),
            "Bomb" => ObjectFactory.CreateBomb(),
            "ClosedDoor" => ObjectFactory.CreateClosedDoor(),
            "OpenDoor" => ObjectFactory.CreateOpenDoor(),
            "TimedBomb" => ObjectFactory.CreateTimedBomb(),
            "Skeleton" => ObjectFactory.CreateSkeleton(),
            "Warlock" => ObjectFactory.CreateWarlock(),
            "BlueLizard" => ObjectFactory.CreateBlueLizard(),
            "RedLizard" => ObjectFactory.CreateRedLizard(),
            "Bat" => ObjectFactory.CreateBat(),
            "Frog" => ObjectFactory.CreateFrog(),
            "Zombie" => ObjectFactory.CreateZombie(),
            _ => throw new Exception($"Unknown object value: {objectValue} at position ({x}, {y})")
        };
        return obj;
    }
}

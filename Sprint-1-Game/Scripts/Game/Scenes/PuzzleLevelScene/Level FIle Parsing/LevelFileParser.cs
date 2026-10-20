using System;
using System.IO;

namespace Scripts.Game;

public class LevelFileParser(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly string Delimiter = ",";
    private readonly string ObjectDelimiter = ".";
    private readonly int NameIdx = 0;
    private readonly int WidthIdx = 1;
    private readonly int HeightIdx = 2;
    private readonly int SpacerRow = 1;
    public string[] ParseLevelFile(string levelFilePath)
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
                IObject @object = ProcessObject(objectValue, x, y);
                Grid grid = new()
                {
                    Tile = tile,
                    Object = @object
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
            "_" => tileFactory.CreateEmptyTile(),
            "Dirt" => tileFactory.CreateDirtTile(),
            "Grass" => tileFactory.CreateGrassTile(),
            "Brick" => tileFactory.CreateBrickTile(),
            "MossyBrick" => tileFactory.CreateMossyBrickTile(),
            "Pit" => tileFactory.CreatePitTile(),
            "Lava" => tileFactory.CreateLavaTile(),
            "Water" => tileFactory.CreateWaterTile(),
            "Bug" => tileFactory.CreateBugTile(),
            "CCBug" => tileFactory.CreateCCBugTile(),
            _ => throw new Exception($"Unknown tile value: {tileValue} at position ({x}, {y})")
        };
        return tile;
    }

    private IObject ProcessObject(string objectValue, int x, int y)
    {
        string[] objectParts = objectValue.Split(ObjectDelimiter);
        string objectType = objectParts[0];
        string direction = objectParts.Length > 1 ? objectParts[1] : null;

        IObject @object = objectType switch
        {
            "_" => objectFactory.CreateEmpty(),
            "Player" => objectFactory.CreatePlayer(),
            "Rock" => objectFactory.CreateDefaultRock(),
            "Wall" => objectFactory.CreateWall(),
            "Bomb" => objectFactory.CreateBomb(),
            "TimedBomb" => objectFactory.CreateTimedBomb(),
            "Skeleton" => objectFactory.CreateSkeleton(),
            "Warlock" => objectFactory.CreateWarlock(),
            "BlueLizard" => objectFactory.CreateBlueLizard(),
            "RedLizard" => objectFactory.CreateRedLizard(),
            _ => throw new Exception($"Unknown object value: {objectValue} at position ({x}, {y})")
        };
        return @object;
    }
}

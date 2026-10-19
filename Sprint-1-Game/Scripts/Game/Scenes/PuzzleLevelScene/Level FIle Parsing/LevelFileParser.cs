using System;
using System.IO;

namespace Scripts.Game;

public class LevelFileParser(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly string Delimiter = ",";
    private readonly int NameIdx = 0;
    private readonly int WidthIdx = 1;
    private readonly int HeightIdx = 2;
    private readonly int SpacerRow = 1;
    public PuzzleLevel ParseLevelFile(string levelFilePath)
    {
        PuzzleLevel level = new(objectFactory, tileFactory, objectSpriteFactory, tileSpriteFactory);
        level.GridPointer.Init();
        string[] lines = File.ReadAllLines(levelFilePath);
        int currentLine = 0;

        string[] headerValues = ParseHeader(lines[currentLine++]);
        level.Context.LevelName = headerValues[NameIdx];
        int levelWidth = int.Parse(headerValues[WidthIdx]);
        int levelHeight = int.Parse(headerValues[HeightIdx]);
        level.Context.LevelWidth = levelWidth;
        level.Context.LevelHeight = levelHeight;

        ParseGrids(level,lines, currentLine, levelWidth, levelHeight);

        level.SpriteGrid.Init(level.GridPointer, level.Context);

        return level;
    }
    private string[] ParseHeader(string headerLine)
    {
        string[] headerParts = headerLine.Split(Delimiter);
        return headerParts[..3];
    }

    private void ParseGrids(PuzzleLevel level, string[] lines, int currentLine, int levelWidth, int levelHeight)
    {
        for (int y = 0; y < levelHeight; y++)
        {
            string tileLine = lines[currentLine + y];
            string objectLine = lines[currentLine + levelHeight + SpacerRow + y];
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
                level.GridPointer.SetGrid(new Coordinate(x, y), grid);
            }
        }
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
        IObject @object = objectValue switch
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

using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class LevelFileParser(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly string Delimiter = ",";
    private const int NameIdx = 0;
    private const int DimensionsIdx = 1;
    private const string DimensionsDelimiter = "x";
    private const int SpacerRow = 1;
    public PuzzleLevel ParseLevelFile(string levelFilePath)
    {
        PuzzleLevel level = new(objectFactory, tileFactory, objectSpriteFactory, tileSpriteFactory);
        string[] lines = File.ReadAllLines(levelFilePath);
        int currentLine = 0;

        string[] headerValues = ParseHeader(lines[currentLine++]);
        string levelName = headerValues[NameIdx];
        int levelWidth = int.Parse(headerValues[DimensionsIdx].Substring(0, headerValues[DimensionsIdx].IndexOf(DimensionsDelimiter)));
        int levelHeight = int.Parse(headerValues[DimensionsIdx].Substring(headerValues[DimensionsIdx].IndexOf(DimensionsDelimiter) + 1));
        level.Context.LevelWidth = levelWidth;
        level.Context.LevelHeight = levelHeight;
        level.Context.LevelName = levelName;

        ParseGrids(level,lines, currentLine, levelWidth, levelHeight);

        return level;
    }
    private string[] ParseHeader(string headerLine)
    {
        string[] headerParts = headerLine.Split(Delimiter);
        return headerParts[..2];
    }

    private void ParseGrids(PuzzleLevel level, string[] lines, int currentLine, int levelWidth, int levelHeight)
    {
        for (int i = 0; i < levelHeight; i++)
        {
            string tileLine = lines[currentLine + i];
            string objectLine = lines[currentLine + levelHeight + SpacerRow + i];
            string[] tileValues = tileLine.Split(Delimiter);
            string[] objectValues = objectLine.Split(Delimiter);
            for (int j = 0; j < levelWidth; j++)
            {
                string tileValue = tileValues[j];
                string objectValue = objectValues[j];
                ITile tile = ProcessTile(tileValue, j, i);
                IObject @object = ProcessObject(objectValue, j, i);
                Grid grid = new()
                {
                    Tile = tile,
                    Object = @object
                };
                level.GridPointer.SetGrid(new Coordinate(j, i), grid);
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

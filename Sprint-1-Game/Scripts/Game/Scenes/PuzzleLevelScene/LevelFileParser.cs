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
    public PuzzleLevel parseLevelFile(string levelFilePath)
    {
        PuzzleLevel level = new(objectFactory, tileFactory, objectSpriteFactory, tileSpriteFactory);
        string[] lines = File.ReadAllLines(levelFilePath);
        int currentLine = 0;

        string[] headerValues = parseHeader(lines[currentLine++]);
        int levelWidth = int.Parse(headerValues[DimensionsIdx].Substring(0, headerValues[DimensionsIdx].IndexOf(DimensionsDelimiter)));
        int levelHeight = int.Parse(headerValues[DimensionsIdx].Substring(headerValues[DimensionsIdx].IndexOf(DimensionsDelimiter) + 1));

        currentLine += SpacerRow;

        parseTileGrid(lines, currentLine, levelWidth, levelHeight);

        currentLine += levelHeight + SpacerRow;

        parseObjectGrid(lines, currentLine, levelWidth, levelHeight);

        return level;
    }
    private string[] parseHeader(string headerLine)
    {
        string[] headerParts = headerLine.Split(Delimiter);
        return headerParts[..2];
    }

    private void parseTileGrid(string[] lines, int currentLine, int levelWidth, int levelHeight)
    {
        for (int i = 0; i < levelHeight; i++)
        {
            string line = lines[currentLine + i];
            string[] tileValues = line.Split(Delimiter);
            for (int j = 0; j < levelWidth; j++)
            {
                string tileValue = tileValues[j];
                ProcessTile(tileValue, j, i);
            }
        }
    }

    private void ProcessTile(string tileValue, int x, int y)
    {
        ITile tile = tileValue switch
        {
            "_" => tileFactory.CreateEmptyTile(),
            "Dirt" => tileFactory.CreateDirtTile(),
            "Pit" => tileFactory.CreatePitTile(),
            "Lava" => tileFactory.CreateLavaTile(),
            "Water" => tileFactory.CreateWaterTile(),
            "Bug" => tileFactory.CreateBugTile(),
            _ => throw new Exception($"Unknown tile value: {tileValue} at position ({x}, {y})")
        };
    }

    private void parseObjectGrid(string[] lines, int currentLine, int levelWidth, int levelHeight)
    {
        for (int i = 0; i < levelHeight; i++)
        {
            string line = lines[currentLine + i];
            string[] objectValues = line.Split(Delimiter);
        }
    }
}

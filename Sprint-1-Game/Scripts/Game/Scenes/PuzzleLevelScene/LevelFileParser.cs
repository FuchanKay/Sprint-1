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

        string[] headerValues = parseHeader(lines[currentLine]);
        currentLine += 2;
        int levelWidth = int.Parse(headerValues[DimensionsIdx].Substring(0, headerValues[DimensionsIdx].IndexOf(DimensionsDelimiter)));
        int levelHeight = int.Parse(headerValues[DimensionsIdx].Substring(headerValues[DimensionsIdx].IndexOf(DimensionsDelimiter) + 1));

        return level;
    }
    private string[] parseHeader(string headerLine)
    {
        string[] headerParts = headerLine.Split(Delimiter);
        return headerParts[..2];
    }

    private void parseTileGrid(string[] lines, int currentLine, int levelHeight)
    {
        for (; currentLine < levelHeight; currentLine++)
        {
            string line = lines[currentLine];
            string[] tileValues = line.Split(Delimiter);
        }
    }
}

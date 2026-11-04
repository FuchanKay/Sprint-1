using System;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class LevelEditorSceneController(ISceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level;
    private LevelContext Context = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 50,
        PuzzleOffsetY = 50,
    };
    private GridPointer Grid = new();
    private readonly ObjectFactory ObjectFactory = new();
    private readonly TileFactory TileFactory = new();
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    private SpriteGrid SpriteGrid;
    public int CurrentLevelIndex = 0;

    public void Init()
    {
        Grid.Init();
        var levelFileName = FileNames.LevelFileNames[0];
        string[] levelFile = LevelFileParser.ParseLevelFile(levelFileName);
        LevelFileParser levelFileParser = new();
        Context.LevelName = levelFileParser.GetLevelName(levelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(levelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(levelFile);
        Grid = levelFileParser.CreateGrids(levelFile);
        SpriteGrid = new(ObjectSpriteFactory, TileSpriteFactory);
        SpriteGrid.Init(Grid, Context);
    }
    public void Update(int dtMs)
    {
        SpriteGrid.Update(dtMs);

        if (mouseInput.IsHeld(InputNames.Select))
        {
            var leftmostPoint = Context.PuzzleOffsetX;
            var rightmostPoint = (int) (Context.LevelWidth * Context.RawSpriteWidthPx * Context.GridScale + Context.PuzzleOffsetX);
            var topMostPoint = Context.PuzzleOffsetY;
            var bottomMostPoint = (int) (Context.LevelWidth * Context.RawSpriteHeightPx * Context.GridScale + Context.PuzzleOffsetY);

            var gridWidth = (int) (Context.RawSpriteWidthPx * Context.GridScale);
            var gridHeight = (int) (Context.RawSpriteHeightPx * Context.GridScale);

            var mouseX = mouseInput.MousePositionX;
            var mouseY = mouseInput.MousePositionY;

            var outOfBounds = mouseX < leftmostPoint || mouseX > rightmostPoint || mouseY < topMostPoint || mouseY > bottomMostPoint;
            if (outOfBounds)
            {
                Console.WriteLine("Out Of Bounds!");
            }
            else
            {
                var coordX = (mouseX - leftmostPoint) / gridWidth;
                var coordY = (mouseY - topMostPoint) / gridHeight;
                Console.WriteLine($"({coordX}, {coordY})");
            }
        }
    }
    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
    }
}
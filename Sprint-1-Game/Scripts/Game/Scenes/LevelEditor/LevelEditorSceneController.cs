using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class LevelEditorSceneController(ISceneManager sceneManager, IInputManager buttonInput, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level;
    private LevelContext Context = new()
    {
        GridSpriteWidthPx = 256,
        GridSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 50,
        PuzzleOffsetY = 50,
    };
    private LevelFileParser LevelFileParser;
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



    }
    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
    }
}
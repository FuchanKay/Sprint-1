using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class LevelEditorSceneController(ISceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, ITextureAtlas textureAtlas) : ISceneController
{
    private LevelContext Context = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 50,
        PuzzleOffsetY = 50,
    };
    private GridPointer Grid = new();
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    private readonly EffectSpriteFactory EffectSpriteFactory = new(textureAtlas);
    private SpriteGrid SpriteGrid;
    private readonly ObjectPaintBrush Brush = new(buttonInput, mouseInput);
    public int CurrentLevelIndex = 0;

    public void Init()
    {
        Grid.Init();

        var levelFileName = FileNames.LevelFileNames[1];
        string[] levelFile = LevelFileParser.ParseLevelFile(levelFileName);
        LevelFileParser levelFileParser = new();
        Context.LevelName = levelFileParser.GetLevelName(levelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(levelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(levelFile);
        Grid = levelFileParser.CreateGrids(levelFile);

        SpriteGrid = new(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory);
        SpriteGrid.Init(Grid, Context);

        Brush.Init(Grid, SpriteGrid, ObjectSpriteFactory, Context);
    }

    public void Update(int dtMs)
    {
        SpriteGrid.Update(dtMs);
        Brush.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
        Brush.Draw(sb);
    }
}
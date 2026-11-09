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
    private IBrush Brush;
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

        Brush = new ObjectPaintBrush(buttonInput, mouseInput, Grid, SpriteGrid, Context, ObjectSpriteFactory);
        Brush.Init();
    }

    public void Update(int dtMs)
    {
        if (buttonInput.IsPressed(InputNames.EraserBrushMode))
        {
            Brush = new EraserBrush(mouseInput, Grid, SpriteGrid, Context);
            Brush.Init();
        }
        else if (buttonInput.IsPressed(InputNames.ObjectPaintBrushMode))
        {
            Brush = new ObjectPaintBrush(buttonInput, mouseInput, Grid, SpriteGrid, Context, ObjectSpriteFactory);
            Brush.Init();
        }
        SpriteGrid.Update(dtMs);
        Brush.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
        Brush.Draw(sb);
    }
}
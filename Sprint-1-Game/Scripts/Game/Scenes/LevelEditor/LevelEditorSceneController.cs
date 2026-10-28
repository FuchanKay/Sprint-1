using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class LevelEditorSceneController(ISceneManager sceneManager, IInputManager buttonInput, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level;
    private LevelFileParser LevelFileParser;
    private readonly ObjectFactory ObjectFactory = new();
    private readonly TileFactory TileFactory = new();
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    public int CurrentLevelIndex = 0;

    public void Init()
    {



    }
    public void Update(int dtMs)
    {
        




    }
    public void Draw(SpriteBatch sb)
    {
        

    }
}
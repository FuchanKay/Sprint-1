using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;
public class PlayGameButton(SceneManager sm) : Button
{
    private readonly SceneManager SceneManager = sm;
    public static Texture2D ButtonTexture { get; set; }
    protected override Texture2D Texture => ButtonTexture;

    protected override void OnClick()
    {
        SceneManager.SwapScene(GameplaySceneController.Name);
    }
}
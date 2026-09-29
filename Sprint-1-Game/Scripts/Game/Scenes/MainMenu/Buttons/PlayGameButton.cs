using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;
public class PlayGameButton(SceneManager sm, IAudioManager audioManager) : Button
{
    private readonly SceneManager SceneManager = sm;
    private readonly IAudioManager AudioManager = audioManager;
    public static Texture2D ButtonTexture { get; set; }
    protected override Texture2D Texture => ButtonTexture;

    protected override void OnClick()
    {
        SceneManager.SwapScene(GameplaySceneController.Name);
        // Placeholder sound effect to indicate that the button was clicked.
        AudioManager.PlaySound("snap");
    }
}
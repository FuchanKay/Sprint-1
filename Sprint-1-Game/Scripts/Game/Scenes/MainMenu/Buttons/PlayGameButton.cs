using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PlayGameButton(ISceneManager sm, IAudioManager audioManager) : Button
{
    public static Texture2D ButtonTexture { get; set; }
    protected override Texture2D Texture => ButtonTexture;

    protected override void OnClick()
    {
        sm.SwapScene(GameplaySceneController.Name);
        // Placeholder sound effect to indicate that the button was clicked.
        audioManager.PlaySound("snap");
    }
}
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PlayGameButton(ISceneManager sm, IAudioManager audioManager, ITextureAtlas textureAtlas) : Button
{
    protected override Texture2D Texture => textureAtlas.GetTexture(TextureNames.PlayGameButton);

    protected override void OnClick()
    {
        sm.SwapScene(SceneNames.PuzzleLevel);
        // Placeholder sound effect to indicate that the button was clicked.
        audioManager.PlaySound(SoundNames.Snap);
    }
}
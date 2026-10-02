using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ExitGameButton(ISceneManager sm, ITextureAtlas textureAtlas) : Button
{
    protected override Texture2D Texture => textureAtlas.GetTexture(TextureNames.ExitGameButton);
    protected override void OnClick()
    {
        sm.ExitGame();
    }
}
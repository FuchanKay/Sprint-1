using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class LevelEditorButton(ISceneManager sm, ITextureAtlas textureAtlas) : Button
{
    protected override Texture2D Texture => textureAtlas.GetTexture(TextureNames.LevelEditorButton);

    protected override void OnClick()
    {
        sm.SwapScene(SceneNames.LevelEditor);
    }
}
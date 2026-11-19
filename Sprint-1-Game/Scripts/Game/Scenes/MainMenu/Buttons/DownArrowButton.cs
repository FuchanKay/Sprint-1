using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
using SharpCompress.Common;

namespace Scripts.Game;

public class DownArrowButton(ISceneManager sm, IAudioManager audioManager, ITextureAtlas textureAtlas, Grid flag) : Button
{
    protected override Texture2D Texture => textureAtlas.GetTexture(TextureNames.LevelEditorButton);

    protected override void OnClick()
    {
        flag.Object = new BombObject();
        audioManager.PlaySound(SoundNames.Snap);
    }
}
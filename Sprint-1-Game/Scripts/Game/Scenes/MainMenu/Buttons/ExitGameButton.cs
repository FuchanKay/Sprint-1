using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ExitGameButton(ISceneManager sm) : Button
{
    public static Texture2D ButtonTexture { get; set; }
    protected override Texture2D Texture => ButtonTexture;

    protected override void OnClick()
    {
        sm.ExitGame();
    }
}
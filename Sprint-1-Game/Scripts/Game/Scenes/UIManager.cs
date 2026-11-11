
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.GameComponents;

namespace Scripts.Game;
public class UIManager(UIFontAtlas uIFontAtlas)
{
    private HUDPannel HUDPannel = new(uIFontAtlas);
    public void Init(PuzzleLevel level)
    {
        HUDPannel.Init(level);
    }
    public void Update(int dtMs)
    {
        HUDPannel.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        HUDPannel.Draw(sb);
    }
}
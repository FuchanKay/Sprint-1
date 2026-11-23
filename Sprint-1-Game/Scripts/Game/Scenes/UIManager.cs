
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.GameComponents;

namespace Scripts.Game;

public class UIManager(GameComponents.UIFontAtlas uIFontAtlas)
{
    private HUDPanel HUDPanel = new(uIFontAtlas);
    public void Init(PuzzleLevel level)
    {
        HUDPanel.Init(level);
    }
    public void Update(int dtMs)
    {
        HUDPanel.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        HUDPanel.Draw(sb);
    }
}
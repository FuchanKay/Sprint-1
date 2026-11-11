
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;
public class UIManager(SpriteFontManager spriteFontManager)
{
    private HUDPannel HUDPannel = new(spriteFontManager);
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
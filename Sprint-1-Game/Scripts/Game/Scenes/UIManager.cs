
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.GameComponents;

namespace Scripts.Game;
public class UIManager(UIFontAtlas uIFontAtlas, UITextureAtlas uITextureAtlas, GameLayoutManager gameLayoutManager)
{
    private HUDPannel HUDPannel = new(uIFontAtlas, uITextureAtlas);
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
        gameLayoutManager.HUDLayout(HUDPannel);

        HUDPannel.Draw(sb);
    }
}
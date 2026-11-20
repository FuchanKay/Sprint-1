using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FogManager
{
    private Vector2 PlayerSpritePos;
    private Effect FogOfWarEffect;
    #nullable enable
    private Texture2D? Fog;

    public void Init(Vector2 playerSpritePos, Effect fogOfWarEffect)
    {
        PlayerSpritePos = playerSpritePos;
        FogOfWarEffect = fogOfWarEffect;
    }

    public void Update(Vector2 playerSpritePos)
    {
        PlayerSpritePos = playerSpritePos;
    }

    public void Draw(SpriteBatch sb)
    {
        var viewport = sb.GraphicsDevice.Viewport;
        if(Fog == null) Fog = new Texture2D(sb.GraphicsDevice, 1, 1);
        FogOfWarEffect.Parameters["PlayerPosition"].SetValue(PlayerSpritePos);
        FogOfWarEffect.Parameters["ScreenSize"].SetValue(new Vector2(viewport.Width, viewport.Height));
        FogOfWarEffect.Parameters["Radius"].SetValue(120f);
        FogOfWarEffect.Parameters["FadeWidth"].SetValue(40f);

        sb.End();

        sb.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, effect: FogOfWarEffect);
        sb.Draw(
        Fog,
        new Rectangle(0, 0, viewport.Width, viewport.Height),
        null,
        Color.Black,
        0f,
        Vector2.Zero,
        SpriteEffects.None,
        0.95f);
        sb.End();

        sb.Begin(SpriteSortMode.FrontToBack);
    }
}
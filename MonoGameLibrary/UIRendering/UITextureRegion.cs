using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MonoGameLibrary.UIRendering;

public class UITextureRegion 
{
    public Texture2D Texture { get; set; }
    public Rectangle SourceRectangle { get; set; }
    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;
    public UITextureRegion() { }
    public UITextureRegion(Texture2D texture, int x, int y, int width, int height)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(x, y, width, height);
    }
    public void Draw(SpriteBatch sb, Vector2 position, Color color)
    {
        Draw(sb, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }
    public void Draw(SpriteBatch sb, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
        Draw(
            sb,
            position,
            color,
            rotation,
            origin,
            new Vector2(scale, scale),
            effects,
            layerDepth
        );
    }
    public void Draw(SpriteBatch sb, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        sb.Draw(
            Texture,
            position,
            SourceRectangle,
            color,
            rotation,
            origin,
            scale,
            effects,
            layerDepth
        );
    }

}
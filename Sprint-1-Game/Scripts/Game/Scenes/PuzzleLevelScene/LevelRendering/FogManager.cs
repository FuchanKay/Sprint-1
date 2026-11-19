using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FogManager
{
    private Vector2 PlayerSpritePos;
    private Color[] pixels;
    #nullable enable
    private Texture2D? Fog;

    public void Init(Vector2 playerSpritePos)
    {
        PlayerSpritePos = playerSpritePos;
    }

    public void Update(Vector2 playerSpritePos)
    {
        PlayerSpritePos = playerSpritePos;
    }

    public void Draw(SpriteBatch sb)
    {
        var viewport = sb.GraphicsDevice.Viewport;
        var width = viewport.Width;
        var height = viewport.Height;
        if(Fog == null)
        {
            Fog = new Texture2D(sb.GraphicsDevice, width, height);
            pixels = new Color[width * height];
        } 

        float radius = 120f;
        float fadeWidth = 40f;

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                float dx = x - PlayerSpritePos.X;
                float dy = y - PlayerSpritePos.Y;
                float distance = MathF.Sqrt(dx * dx + dy * dy);

                float alpha = MathHelper.Clamp((distance - radius) / fadeWidth, 0f, 1f);
                pixels[y * width + x] = new Color((byte)0, (byte)0, (byte)0, (byte)(alpha * 255));
            }
        }
        Fog.SetData(pixels);

        sb.Draw(
        Fog,
        new Rectangle(0, 0, viewport.Width, viewport.Height),
        null,
        Color.Black,
        0f,
        Vector2.Zero,
        SpriteEffects.None,
        0.95f);
    }
}
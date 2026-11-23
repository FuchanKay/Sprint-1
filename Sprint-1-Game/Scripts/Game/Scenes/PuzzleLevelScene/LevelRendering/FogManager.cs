using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

public class FogManager
{
    private ISprite PlayerSprite;
    private Effect FogOfWarEffect;
    private Color[] ExploredPixels = [];
    private float Radius;
    private float FadeWidth;
    private bool LeaveTrail = false;
    #nullable enable
    private Texture2D? ExploredMask;
    private Vector2? LastRecordedPosition;

    public void Init(ISprite playerSprite, Effect fogOfWarEffect, bool leaveTrail)
    {
        PlayerSprite = playerSprite;
        LastRecordedPosition = null;
        FogOfWarEffect = fogOfWarEffect;
        LeaveTrail = leaveTrail;
        ExploredMask = null;
        Radius = 85f * PlayerSprite.Scale;
        FadeWidth = Radius/3f;
    }

    public void Update(ISprite playerSprite)
    {
        PlayerSprite = playerSprite;
    }

    public void Draw(SpriteBatch sb)
    {
        var viewport = sb.GraphicsDevice.Viewport;
        if(ExploredMask == null)
        {
            ExploredMask = new Texture2D(sb.GraphicsDevice, viewport.Width, viewport.Height);
            ExploredPixels = new Color[viewport.Width * viewport.Height];
        }    
        if (LeaveTrail) UpdateExploredArea(PlayerSprite.CurrentPosition, viewport.Width, viewport.Height);
        FogOfWarEffect.Parameters["ExploredTexture"].SetValue(ExploredMask);
        FogOfWarEffect.Parameters["PlayerPosition"].SetValue(PlayerSprite.CurrentPosition);
        FogOfWarEffect.Parameters["ScreenSize"].SetValue(new Vector2(viewport.Width, viewport.Height));
        FogOfWarEffect.Parameters["Radius"].SetValue(Radius);
        FogOfWarEffect.Parameters["FadeWidth"].SetValue(FadeWidth);

        sb.End();

        sb.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, effect: FogOfWarEffect);
        sb.Draw(
        ExploredMask,
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

    private void UpdateExploredArea(Vector2 center, int width, int height)
    {
        if (LastRecordedPosition == center) return;

        int left = Math.Max(0, (int)(center.X - Radius - FadeWidth));
        int right = Math.Min(width - 1, (int)(center.X + Radius + FadeWidth));
        int top = Math.Max(0, (int)(center.Y - Radius - FadeWidth));
        int bottom = Math.Min(height - 1, (int)(center.Y + Radius + FadeWidth));

        for (int y = top; y <= bottom; y++)
        {
            for (int x = left; x <= right; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                float visibility = 1f - MathHelper.Clamp((distance - Radius) / FadeWidth, 0f, 1f);

                int index = y * width + x;
                byte value = (byte)(visibility * 255);

                if (value > ExploredPixels[index].R)
                    ExploredPixels[index] = new Color(value, value, value, (byte)255);
            }
        }

        ExploredMask!.SetData(ExploredPixels);
        LastRecordedPosition = center;
    }
}
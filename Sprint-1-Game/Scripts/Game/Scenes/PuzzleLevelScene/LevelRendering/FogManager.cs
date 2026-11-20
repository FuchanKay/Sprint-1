using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FogManager
{
    private Vector2 PlayerSpritePos;
    private Effect FogOfWarEffect;
    private Color[] ExploredPixels = [];
    private const float Radius = 120f;
    private const float FadeWidth = 40f;
    private bool LeaveTrail = false;
    #nullable enable
    private Texture2D? ExploredMask;
    private Vector2? LastRecordedPosition;

    public void Init(Vector2 playerSpritePos, Effect fogOfWarEffect, bool leaveTrail)
    {
        PlayerSpritePos = playerSpritePos;
        LastRecordedPosition = null;
        FogOfWarEffect = fogOfWarEffect;
        LeaveTrail = leaveTrail;
    }

    public void Update(Vector2 playerSpritePos)
    {
        PlayerSpritePos = playerSpritePos;
    }

    public void Draw(SpriteBatch sb)
    {
        var viewport = sb.GraphicsDevice.Viewport;
        if(ExploredMask == null)
        {
            ExploredMask = new Texture2D(sb.GraphicsDevice, viewport.Width, viewport.Height);
            ExploredPixels = new Color[viewport.Width * viewport.Height];
        }    
        if (LeaveTrail) UpdateExploredArea(PlayerSpritePos, viewport.Width, viewport.Height);
        FogOfWarEffect.Parameters["ExploredTexture"].SetValue(ExploredMask);
        FogOfWarEffect.Parameters["PlayerPosition"].SetValue(PlayerSpritePos);
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
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class GameLayoutManager
{
    private const float SceneRatio = 16f / 9f;
    private const float LevelSceneRatio = 4f / 5f;
    private const float HUDSceneRatio = 1f / 5f;
    public LevelContext Context;
    private int CurrentSceneWidth;
    private int CurrentSceneHeight;

    public void Initialize(GameWindow window)
    {
        CurrentSceneWidth = window.ClientBounds.Width;
        CurrentSceneHeight = window.ClientBounds.Height;
    }
    public void LevelLayout(LevelContext context)
    {
        Context = context;
        float levelWidthPx = Context.LevelWidth * Context.GridSpriteWidthPx;
        float levelHeightPx = Context.LevelHeight * Context.GridSpriteHeightPx;
        ScaleLevel(levelWidthPx, levelHeightPx);
        AnchorLevel(levelWidthPx, levelHeightPx);


    }
    public void HUDLayoutInitialize(LevelContext context)
    {
        Context = context;
    }

    private void AnchorLevel(float levelWidthPx, float levelHeightPx)
    {
        int EdgeOffset = Context.GridSpriteHeightPx / 2;
        Context.PuzzleOffsetX = (int)((float)((CurrentSceneWidth * LevelSceneRatio - levelWidthPx * Context.GridScale) / 2f) + EdgeOffset * Context.GridScale);
        Context.PuzzleOffsetY = (int)((float)((CurrentSceneHeight - levelHeightPx * Context.GridScale) / 2f) + EdgeOffset * Context.GridScale);
    }
    private void ScaleLevel(float levelWidthPx, float levelHeightPx)
    {
        float scaleX = CurrentSceneWidth * LevelSceneRatio / levelWidthPx;
        float scaleY = CurrentSceneHeight / levelHeightPx;
        Context.GridScale = Math.Min(scaleX, scaleY);
    }
}
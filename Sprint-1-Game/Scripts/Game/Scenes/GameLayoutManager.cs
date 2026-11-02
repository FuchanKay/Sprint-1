using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class GameLayoutManager
{
    private const float LevelSceneRatio = 4f / 5f;
    private const float HUDSceneRatio = 1f / 5f;
    public LevelContext LevelContext;
    //place holder, will be implemented in the future
    public LevelContext HUDContext;

    private int CurrentSceneWidth;
    private int CurrentSceneHeight;

    public void Init(GameWindow window)
    {
        CurrentSceneWidth = window.ClientBounds.Width;
        CurrentSceneHeight = window.ClientBounds.Height;
    }

    public void LevelLayout(LevelContext context)
    {
        LevelContext = context;
        float levelWidthPx = LevelContext.LevelWidth * LevelContext.GridSpriteWidthPx;
        float levelHeightPx = LevelContext.LevelHeight * LevelContext.GridSpriteHeightPx;
        ScaleLevel(levelWidthPx, levelHeightPx);
        SetLevelOffset(levelWidthPx, levelHeightPx);
    }

    private void SetLevelOffset(float levelWidthPx, float levelHeightPx)
    {
        int EdgeOffset = LevelContext.GridSpriteHeightPx / 2;
        LevelContext.PuzzleOffsetX = (int)Math.Floor((CurrentSceneWidth * LevelSceneRatio - levelWidthPx * LevelContext.GridScale) / 2f + EdgeOffset * LevelContext.GridScale);
        LevelContext.PuzzleOffsetY = (int)Math.Floor((CurrentSceneHeight - levelHeightPx * LevelContext.GridScale) / 2f + EdgeOffset * LevelContext.GridScale);
    }
    private void ScaleLevel(float levelWidthPx, float levelHeightPx)
    {
        float scaleX = CurrentSceneWidth * LevelSceneRatio / levelWidthPx;
        float scaleY = CurrentSceneHeight / levelHeightPx;
        LevelContext.GridScale = Math.Min(scaleX, scaleY);
    }
    //place holder, will be implemented in the future
    public void HUDLayout(LevelContext context)
    {
        HUDContext = context;
    }
}
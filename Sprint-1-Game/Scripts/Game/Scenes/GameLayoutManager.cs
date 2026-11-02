using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class GameLayoutManager
{
    private const float SceneRatio = 16f / 9f;
    private const float LevelSceneRatio = 4f / 5f;
    private const float HUDSceneRatio = 1f / 5f;
    public LevelContext LevelContext;
    public HUDContext HUDContext;

    private int CurrentSceneWidth;
    private int CurrentSceneHeight;
    
    public void Initialize(GameWindow window)
    {
        CurrentSceneWidth = window.ClientBounds.Width;
        CurrentSceneHeight = window.ClientBounds.Height;
    }
    
    public void LevelLayout(LevelContext context)
    {
        LevelContext = context;
        float levelWidthPx = LevelContext.LevelWidth*LevelContext.GridSpriteWidthPx;
        float levelHeightPx = LevelContext.LevelHeight*LevelContext.GridSpriteHeightPx;
        ScaleLevel(levelWidthPx, levelHeightPx);
        AnchorLevel(levelWidthPx, levelHeightPx);
    }

    private void AnchorLevel(float levelWidthPx, float levelHeightPx)
    {
        int EdgeOffset =  LevelContext.GridSpriteHeightPx / 2;
        LevelContext.PuzzleOffsetX = (int)((float)((CurrentSceneWidth*LevelSceneRatio - levelWidthPx*LevelContext.GridScale) / 2f) + EdgeOffset * LevelContext.GridScale);
        LevelContext.PuzzleOffsetY = (int)((float)((CurrentSceneHeight - levelHeightPx*LevelContext.GridScale) / 2f) + EdgeOffset * LevelContext.GridScale);
    }
    private void ScaleLevel(float levelWidthPx, float levelHeightPx)
    {
        float scaleX = CurrentSceneWidth*LevelSceneRatio/levelWidthPx;
        float scaleY = CurrentSceneHeight/levelHeightPx;
        LevelContext.GridScale= Math.Min(scaleX, scaleY);
    }
    public void HUDLayout(HUDContext context)
    {
        HUDContext = context;
    }
}
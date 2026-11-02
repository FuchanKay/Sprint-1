using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class HUD(GameLayoutManager gameLayoutManager)
{
    public string HUDName { get; set; }
    public int GridSpriteWidthPx { get; set; }
    public int GridSpriteHeightPx { get; set; }
    public int HUDOffsetX { get; set; }
    public int HUDOffsetY { get; set; }
    public float GridScale { get; set; }
    public HUDContext Context;
    private PuzzleLevel PuzzleLevel;

    public void Initialize(PuzzleLevel puzzleLevel)
    {
        PuzzleLevel = puzzleLevel;
    }

    public void Update(int dtMs)
    {
        
    }

    public void Draw(SpriteBatch sb)
    {
        
    }

}
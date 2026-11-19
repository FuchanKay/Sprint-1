using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.Game;
using Scripts.GameComponents;

public class HUDPannel(UIFontAtlas uIFontAtlas)
{
    public Vector2 PannelCornerPos = Vector2.Zero;
    private int Clock;
    private UIFont EnemyCounter = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont InventoryStatus = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont GameClock = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont CommandInstructions = new (uIFontAtlas.GetFont(FontNames.DungeonFont));

    private PuzzleLevel Level;

    public void Init(PuzzleLevel level)
    {
        Vector2 inventoryScale = new Vector2(0.25f, 0.25f);
        

        Clock = 0;;
        Level = level;
        EnemyCounter.Init(PannelCornerPos + new Vector2 (0,120), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.Gold, Vector2.One);
        GameClock.Init(PannelCornerPos + new Vector2(0,80), "Game Clock: ", Color.Gold, Vector2.One);
        InventoryStatus.Init(PannelCornerPos + new Vector2 (0,160), $"Inventory:\n\n\n\n    1        2        3",  Color.Gold, Vector2.One);
        CommandInstructions.Init(PannelCornerPos + new Vector2(0,300), "Player Move: WASD\n" 
                                                        + "SNAP: F\n"
                                                        + "Use Item: 123\n" 
                                                        + "Grab item: G\n"
                                                        +"Change Scene: B\n",  Color.Gold, Vector2.One);
        
    }
    public void Update(int dtMs)
    {
        
        EnemyCounter.UpdateText( $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}");

        //This may be moved to PuzzleLevel or LevelStateHistory to count how much time player spent on each puzzle.
        Clock += dtMs;
        TimeSpan formattedClock = TimeSpan.FromMilliseconds(Clock);
        GameClock.UpdateText($"Game Clock: {formattedClock:hh\\:mm\\:ss}");

    }
    public void Draw(SpriteBatch sb)
    {
        UpdateLayout();

        GameClock.Draw(sb);
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);

        CommandInstructions.Draw(sb);

    }

    private void UpdateLayout()
    {

        EnemyCounter.Pos = PannelCornerPos + new Vector2 (0,120);
        GameClock.Pos = PannelCornerPos + new Vector2(0,80);
        InventoryStatus.Pos = PannelCornerPos + new Vector2 (0,160);
        CommandInstructions.Pos = PannelCornerPos + new Vector2(0,300);
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.Game;
using Scripts.GameComponents;

public class HUDPannel(UIFontAtlas uIFontAtlas)
{
    public Vector2 PannelCornerPos{ get; set; }
    private int Clock;
    private UIFont EnemyCounter = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont InventoryStatus = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont GameClock = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont CommandInstructions = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    
    private PuzzleLevel Level;

    public void Init(PuzzleLevel level)
    {
        Clock = 0;;
        Level = level;
        // EnemyCounter.Init(FontNames.DungeonFont, new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.Gold);
        // GameClock.Init(FontNames.DungeonFont, new Vector2 (1040,80), "Game Clock: ", Color.Gold);
        // InventoryStatus.Init(FontNames.DungeonFont, new Vector2(1040,120), $"Inventory:\n{Level.LevelStateHistory.InventoryManager}",  Color.Gold);
        // CommandInstructions.Init(FontNames.DungeonFont, new Vector2(1040,500), "Player Move: WASD\n" 
        //                                                                         + "SNAP: F\n"
        //                                                                         + "Use Item: 123\n" 
        //                                                                         + "Grab item: G\n"
        //                                                                         +"Change Scene: B\n",  Color.Gold);
        EnemyCounter.Init(new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.Gold, Vector2.One);
        GameClock.Init(new Vector2 (1040,80), "Game Clock: ", Color.Gold, Vector2.One);
        InventoryStatus.Init(new Vector2(1040,120), $"Inventory:\n{Level.LevelStateHistory.InventoryManager}",  Color.Gold, Vector2.One);
        CommandInstructions.Init(new Vector2(1040,500), "Player Move: WASD\n" 
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
        GameClock.UpdateText($"{formattedClock:hh\\:mm\\:ss}");

        InventoryStatus.UpdateText($"Inventory:\n{Level.LevelStateHistory.InventoryManager}");
    }
    public void Draw(SpriteBatch sb)
    {
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);
        GameClock.Draw(sb);
        CommandInstructions.Draw(sb);
    }
}
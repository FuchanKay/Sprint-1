using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
using Scripts.GameComponents;

public class HUDPanel(UIFontAtlas uIFontAtlas)
{
    public Vector2 PannelCornerPos { get; set; }
    private int Clock;
    private UIFont EnemyCounter = new(uIFontAtlas);
    private UIFont InventoryStatus = new(uIFontAtlas);
    private UIFont GameClock = new(uIFontAtlas);
    private UIFont CommandInstructions = new(uIFontAtlas);

    private PuzzleLevel Level;

    public void Init(PuzzleLevel level)
    {
        Clock = 0;
        Level = level;
        EnemyCounter.Init(FontNames.DungeonFont);
        GameClock.Init(FontNames.DungeonFont);
        InventoryStatus.Init(FontNames.DungeonFont);
        CommandInstructions.Init(FontNames.DungeonFont);

        EnemyCounter.Pos = new Vector2(1040, 40);
        EnemyCounter.Color = Color.Gold;
        EnemyCounter.Text= $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}";

        GameClock.Pos = new Vector2(1040, 80);
        GameClock.Color = Color.Gold;
        GameClock.Text = "00:00:00";

        InventoryStatus.Pos = new Vector2(1040, 120);
        InventoryStatus.Color = Color.Gold;
        InventoryStatus.Text = $"Inventory:\n{Level.LevelStateHistory.InventoryManager}";
        
        CommandInstructions.Pos = new Vector2(1040, 350);
        CommandInstructions.Text = "Player Move: WASD\n"
                                    + "SNAP: F\n"
                                    + "Use Item: 123\n"
                                    + "Grab item: G\n"
                                    + "Toggle Hide Objects: H\n"
                                    + "Change Scene: B\n";
        CommandInstructions.Color = Color.Gold;


    }
    public void Update(int dtMs)
    {
        EnemyCounter.Text= $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}";

        //This may be moved to PuzzleLevel or LevelStateHistory to count how much time player spent on each puzzle.
        Clock += dtMs;
        TimeSpan formattedClock = TimeSpan.FromMilliseconds(Clock);
        GameClock.Text = $"{formattedClock:hh\\:mm\\:ss}";

        InventoryStatus.Text = $"Inventory:\n{Level.LevelStateHistory.InventoryManager}";
    }
    public void Draw(SpriteBatch sb)
    {
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);
        GameClock.Draw(sb);
        CommandInstructions.Draw(sb);
    }
}
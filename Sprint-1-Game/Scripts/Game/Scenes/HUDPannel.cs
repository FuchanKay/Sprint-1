using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Scripts.Game;
using Scripts.GameComponents;

public class HUDPannel(SpriteFontManager spriteFontManager)
{
    public Vector2 PannelCornerPos{ get; set; }
    private int Clock;
    private BasicSpriteFontWriter EnemyCounter = new (spriteFontManager);
    private BasicSpriteFontWriter InventoryStatus = new (spriteFontManager);
    private BasicSpriteFontWriter GameClock = new (spriteFontManager);
    private BasicSpriteFontWriter CommandInstructions = new (spriteFontManager);
    
    private PuzzleLevel Level;

    public void Init(PuzzleLevel level)
    {
        Clock = 0;;
        Level = level;
        EnemyCounter.Init(FileNames.DungeontFont, new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.Gold);
        GameClock.Init(FileNames.DungeontFont, new Vector2 (1040,80), "Game Clock: ", Color.Gold);
        InventoryStatus.Init(FileNames.DungeontFont, new Vector2(1040,120), $"Inventory:\n{Level.LevelStateHistory.InventoryManager}",  Color.Gold);
        CommandInstructions.Init(FileNames.DungeontFont, new Vector2(1040,500), "Player Move: WASD\n" 
                                                                                + "SNAP: F\n"
                                                                                + "Use Item: 123\n" 
                                                                                + "Grab item: G\n"
                                                                                +"Change Scene: B\n",  Color.Gold);
        
    }
    public void Update(int dtMs)
    {
        EnemyCounter.Update(new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}");

        //This may be moved to PuzzleLevel or LevelStateHistory to count how much time player spent on each puzzle.
        Clock += dtMs;
        TimeSpan formattedClock = TimeSpan.FromMilliseconds(Clock);
        GameClock.Update(new Vector2 (1040,80), $"{formattedClock:hh\\:mm\\:ss}");


        InventoryStatus.Update(new Vector2(1040,120), $"Inventory:\n{Level.LevelStateHistory.InventoryManager}");
        CommandInstructions.Update(new Vector2(1040,350));
    }
    public void Draw(SpriteBatch sb)
    {
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);
        GameClock.Draw(sb);
        CommandInstructions.Draw(sb);
    }
}
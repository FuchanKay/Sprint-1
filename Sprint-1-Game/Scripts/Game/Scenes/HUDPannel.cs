using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.Game;
using Scripts.GameComponents;

public class HUDPannel(UIFontAtlas uIFontAtlas, UITextureAtlas uITextureAtlas)
{
    public Vector2 PannelCornerPos = Vector2.Zero;
    private int Clock;
    private UIFont EnemyCounter = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont InventoryStatus = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont GameClock = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UIFont CommandInstructions = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    private UISprite Background;
    private UISprite Inventory1;
    private UISprite Inventory2;
    private UISprite Inventory3;
    private PuzzleLevel Level;

    public void Init(PuzzleLevel level)
    {
        Vector2 inventoryScale = new Vector2(0.25f, 0.25f);
        Inventory1 = new(uITextureAtlas.GetRegion("Bomb"));
        Inventory2 = new(uITextureAtlas.GetRegion("Empty"));
        Inventory3 = new(uITextureAtlas.GetRegion("Empty"));
        Background = new(uITextureAtlas.GetRegion("Background"));

        Inventory1.Scale = inventoryScale;
        Inventory2.Scale = inventoryScale;
        Inventory3.Scale = inventoryScale;
        Background.Scale = new Vector2(1f, 3f);
        Clock = 0;;
        Level = level;
        EnemyCounter.Init( $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.Gold, Vector2.One);
        GameClock.Init( "Game Clock: ", Color.Gold, Vector2.One);
        InventoryStatus.Init( $"Inventory:\n\n\n\n    1        2        3",  Color.Gold, Vector2.One);
        CommandInstructions.Init( "Player Move: WASD\n" 
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
        Inventory1.UpdateRegion(uITextureAtlas.GetRegion(InventoryManager.ObjectIdToString(Level.LevelStateHistory.InventoryManager.PlayerItems[0].Id)));
        Inventory2.UpdateRegion(uITextureAtlas.GetRegion(InventoryManager.ObjectIdToString(Level.LevelStateHistory.InventoryManager.PlayerItems[1].Id)));
        Inventory3.UpdateRegion(uITextureAtlas.GetRegion(InventoryManager.ObjectIdToString(Level.LevelStateHistory.InventoryManager.PlayerItems[2].Id)));
    }
    public void Draw(SpriteBatch sb)
    {
        Background.Draw(sb, PannelCornerPos);
        GameClock.Draw(sb, PannelCornerPos + new Vector2(0,80));
        EnemyCounter.Draw(sb, PannelCornerPos + new Vector2 (0,120));
        InventoryStatus.Draw(sb, PannelCornerPos + new Vector2 (0,160));
        Inventory1.Draw(sb, PannelCornerPos + new Vector2(0, 180));
        Inventory2.Draw(sb, PannelCornerPos + new Vector2(0+64, 180));
        Inventory3.Draw(sb, PannelCornerPos + new Vector2(0+128, 180));
        CommandInstructions.Draw(sb, PannelCornerPos + new Vector2(0,300));

    }
}
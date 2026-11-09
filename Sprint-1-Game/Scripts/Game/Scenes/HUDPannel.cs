using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
using Scripts.GameComponents;

public class HUDPannel(SpriteFontManager spriteFontManager)
{
    public Vector2 PannelCornerPos{ get; set; }
    private BasicSpriteFontWriter EnemyCounter = new (spriteFontManager);
    private BasicSpriteFontWriter InventoryStatus = new (spriteFontManager);
    private BasicSpriteFontWriter CommandInstructions = new (spriteFontManager);
    private PuzzleLevel Level;

    public void Init(PuzzleLevel level)
    {
        Level = level;
        EnemyCounter.Init(FileNames.DungeontFont, new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.Gold);
        InventoryStatus.Init(FileNames.DungeontFont, new Vector2(1040,80), $"Inventory:\n{Level.LevelStateHistory.InventoryManager}",  Color.Gold);
        CommandInstructions.Init(FileNames.DungeontFont, new Vector2(1040,500), "Player Move: WASD\n" 
                                                                                + "SNAP: F\n"
                                                                                + "Use Item: 123\n" 
                                                                                + "Grab item: G\n"
                                                                                +"Change Scene: B\n",  Color.Gold);
        
    }
    public void Update(int dtms)
    {
        EnemyCounter.Update(new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}");
        InventoryStatus.Update(new Vector2(1040,80), $"Inventory:\n{Level.LevelStateHistory.InventoryManager}");
        CommandInstructions.Update(new Vector2(1040,350));
    }
    public void Draw(SpriteBatch sb)
    {
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);
        CommandInstructions.Draw(sb);
    }
}
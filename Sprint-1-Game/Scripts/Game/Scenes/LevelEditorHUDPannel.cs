using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
using Scripts.GameComponents;

/*
**IMPORTANT NOTE** this code is completely temporary and should be removed once UIManager is fully functional
*/

public class LevelEditorHUDPanel(SpriteFontManager spriteFontManager)
{
    public Vector2 PannelCornerPos { get; set; }
    private BasicSpriteFontWriter CommandInstructions = new(spriteFontManager);

    public void Init()
    {
        CommandInstructions.Init(FontNames.DungeonFont, new Vector2(900, 200), "Object Paint Mode: P\n"
                                                                                + "Tile Paint Mode: T\n"
                                                                                + "Eraser Mode: E/Right Click\n"
                                                                                + "Rotate: R\n"
                                                                                + "Cycle Right: D\n"
                                                                                + "Cycle Left: A\n"
                                                                                + "Undo: Z\n"
                                                                                + "Redo: Y\n"
                                                                                + "Play Level: M\n", Color.Gold);

    }
    public void Update(int dtMs)
    {
        CommandInstructions.Update(new Vector2(900, 200));
    }
    public void Draw(SpriteBatch sb)
    {
        CommandInstructions.Draw(sb);
    }
}
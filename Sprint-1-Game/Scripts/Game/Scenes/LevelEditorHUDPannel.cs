using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
using Scripts.GameComponents;

/*
**IMPORTANT NOTE** this code is completely temporary and should be removed once UIManager is fully functional
*/

public class LevelEditorHUDPanel(UIFontAtlas uIFontAtlas)
{
    public Vector2 PannelCornerPos { get; set; }
    private UIFont CommandInstructions = new(uIFontAtlas);

    public void Init()
    {
        CommandInstructions.Init(FontNames.DungeonFont);
        CommandInstructions.Pos = new Vector2(900, 200);
        CommandInstructions.Color = Color.Gold;
        CommandInstructions.Text = "Object Paint Mode: P\n"
                                    + "Tile Paint Mode: T\n"
                                    + "Eraser Mode: E/Right Click\n"
                                    + "Rotate: R\n"
                                    + "Cycle Right: D\n"
                                    + "Cycle Left: A\n"
                                    + "Undo: Z\n"
                                    + "Redo: Y\n"
                                    + "Play Level: M\n";

    }
    public void Update(int dtMs)
    {
    }
    public void Draw(SpriteBatch sb)
    {
        CommandInstructions.Draw(sb);
    }
}
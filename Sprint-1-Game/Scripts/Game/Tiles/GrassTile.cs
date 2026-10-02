using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class GrassTile(Vector2 pos) : Tile(pos)
{
    public override void SnapBehavior()
    {
    }
}
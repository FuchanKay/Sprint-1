using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
namespace Scripts.GameComponents;

public abstract class Tile(Vector2 pos) : ITile
{
    public ISprite TileSprite { get; set; }
    public Vector2 Position { get; set; } = pos;
    public abstract void SnapBehavior();
    public void Update(int dtMs)
    {
        
    }
    public void Draw(SpriteBatch sb)
    {
        
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
namespace Scripts.GameComponents;

public abstract class Tile : ITile
{
    protected Vector2 Position = Vector2.Zero;
    public abstract void Update(int dtMs);
    public abstract void Draw(SpriteBatch sb);
}
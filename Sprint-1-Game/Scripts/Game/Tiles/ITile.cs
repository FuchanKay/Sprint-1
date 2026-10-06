using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;
/// <summary>
/// Interface that handles the basic properties and functionality of a tile in the game.
/// </summary>
public interface ITile
{
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
}
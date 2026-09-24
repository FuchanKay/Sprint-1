using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace Scripts.GameComponents;
/// <summary>
/// Interface that handles the basic properties and functionality of an object in the game.
/// </summary>

public interface IObject
{
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
    void Destroy();
    void MoveUp();
    void MoveDown();
    void MoveLeft();
    void MoveRight();
    void SnapBehavior();
}
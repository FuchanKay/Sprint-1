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
    void Move();
    void SnapBehavior();

    Vector2 position {get; set;}
    bool pushable {get; set;}
    bool destructible {get; set;}
}
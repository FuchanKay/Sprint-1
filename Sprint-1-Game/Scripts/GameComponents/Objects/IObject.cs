using System.Numerics;
/// <summary>
/// Interface that handles the basic properties and functionality of an object in the game.
/// </summary>

public interface IObject
{
    void Update();
    void Draw();
    void Destroy();
    void Move();
    void SnapBehavior();

    // Sprite Parameter will be here in the future
    Vector2 position {get; set;}
    bool pushable {get; set;}
    bool destructible {get; set;}
}
using System.Numerics;

public class Bomb : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = true;
    public bool destructible {get; set;} = true;

    public void Update()
    {
        // Update logic for Bomb
    }

    public void Draw()
    {
        // Draw logic for Bomb
    }

    public void SnapBehavior()
    {
        // Snap behavior logic for Bomb
    }

    public void Destroy()
    {
        // Logic to destroy the Bomb
    }

    public void Move()
    {
        // Move logic for Bomb
    }
}
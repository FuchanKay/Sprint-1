using System.Numerics;

public class TimedBomb : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = true;
    public bool destructible {get; set;} = true;

    public void Update()
    {
        // Update logic for TimedBomb
    }

    public void Draw()
    {
        // Draw logic for TimedBomb
    }

    public void SnapBehavior()
    {
        // Snap behavior logic for TimedBomb
    }

    public void Destroy()
    {
        // Logic to destroy the TimedBomb
    }

    public void Move()
    {
        // Move logic for Bomb
    }
}
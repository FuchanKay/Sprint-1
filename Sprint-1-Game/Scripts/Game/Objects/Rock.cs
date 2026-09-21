using System.Numerics;

public class Rock : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = true;
    public bool destructible {get; set;} = true;

    public void Update()
    {
        // Update logic for Rock
    }

    public void Draw()
    {
        // Draw logic for Rock
    }

    public void SnapBehavior()
    {
    }

    public void Destroy()
    {
        // Logic to destroy the Rock
    }

    public void Move()
    {
        // Move logic for Rock
    }
}
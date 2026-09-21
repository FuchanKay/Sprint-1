using System.Numerics;

public class StoneBlock : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = true;

    public void Update()
    {
        // Update logic for StoneBlock
    }

    public void Draw()
    {
        // Draw logic for StoneBlock
    }

    public void SnapBehavior()
    {
    }

    public void Destroy()
    {
        // Logic to destroy the StoneBlock
    }

    public void Move()
    {
    }
}
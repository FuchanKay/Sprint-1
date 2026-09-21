using System.Numerics;

public class GreaterPowerPylon : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = false;
    public int powerLevel {get; set;} = 3;

    public void Update()
    {
        // Update logic for GreaterPowerPylon
    }

    public void Draw()
    {
        // Draw logic for GreaterPowerPylon
    }

    public void SnapBehavior()
    {
        // Snap behavior logic for GreaterPowerPylon
    }

    public void Destroy()
    {
    }

    public void Move()
    {
    }
}
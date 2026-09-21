using System.Numerics;

public class LesserPowerPylon : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = false;
    public int powerLevel {get; set;} = 2;

    public void Update()
    {
        // Update logic for LesserPowerPylon
    }

    public void Draw()
    {
        // Draw logic for LesserPowerPylon
    }

    public void SnapBehavior()
    {
        // Snap behavior logic for LesserPowerPylon
    }

    public void Destroy()
    {
    }

    public void Move()
    {
    }
}
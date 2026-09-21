using System.Numerics;

public class Vines : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = true;
    public bool grown {get; set;} = false;

    public void Update()
    {
        // Update logic for Vines
    }

    public void Draw()
    {
        // Draw logic for Vines
    }

    public void SnapBehavior()
    {
        // Snap behavior logic for Vines
    }

    public void Destroy()
    {
        // Logic to destroy grown Vines
    }

    public void Move()
    {
    }
}
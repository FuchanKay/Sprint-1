using System.Numerics;

public class ExitDoor : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = false;

    public void Update()
    {
        // Update logic for ExitDoor
    }

    public void Draw()
    {
        // Draw logic for ExitDoor
    }

    public void SnapBehavior()
    {
    }

    public void Destroy()
    {
    }

    public void Move()
    {
    }
}
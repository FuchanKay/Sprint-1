using System.Numerics;

public class StoneWall : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = false;

    public void Update()
    {
        // Update logic for StoneWall
    }

    public void Draw()
    {
        // Draw logic for StoneWall
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
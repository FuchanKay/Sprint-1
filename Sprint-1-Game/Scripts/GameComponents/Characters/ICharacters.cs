using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;
/*Basic functions for characters like players, enemies, and so on*/
public abstract class Character
{
    protected Direction _direction;

    public int Health { get; set; }
    public float Speed { get; set; }
    public Vector2 Pos { get; set; }

    protected Character(Vector2 pos, float speed, Direction direction)
    {
        _direction = direction;
        Pos = pos;
        Speed = speed;
        Health = 100;
    }

    public void MoveNorth()
    {
        _direction = Direction.North;
        Pos = new Vector2(Pos.X, Pos.Y - Speed);
    }

    public void MoveEast()
    {
        _direction = Direction.East;
        Pos = new Vector2(Pos.X + Speed, Pos.Y);
    }

    public void MoveSouth()
    {
        _direction = Direction.South;
        Pos = new Vector2(Pos.X, Pos.Y + Speed);
    }

    public void MoveWest()
    {
        _direction = Direction.West;
        Pos = new Vector2(Pos.X - Speed, Pos.Y);
    }
}
using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;
/*Basic functions for characters like players, enemies, and so on*/
public abstract class Character: ICharacter
{
    protected Direction Direction;

    public float Speed { get; set; }
    public Vector2 Pos { get; set; }

    protected Character(Vector2 pos, float speed, Direction direction)
    {
        Direction = direction;
        Pos = pos;
        Speed = speed;
    }

    public void MoveNorth()
    {
        Direction = Direction.North;
        Pos = new Vector2(Pos.X, Pos.Y - Speed);
    }

    public void MoveEast()
    {
        Direction = Direction.East;
        Pos = new Vector2(Pos.X + Speed, Pos.Y);
    }

    public void MoveSouth()
    {
        Direction = Direction.South;
        Pos = new Vector2(Pos.X, Pos.Y + Speed);
    }

    public void MoveWest()
    {
        Direction = Direction.West;
        Pos = new Vector2(Pos.X - Speed, Pos.Y);
    }

}
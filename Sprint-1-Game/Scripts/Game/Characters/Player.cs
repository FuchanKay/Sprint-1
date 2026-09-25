using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Player : ICharacter
{
    private StateMachine stateMachine;
    public Player(Vector2 pos, float speed)
    {
        stateMachine = new StateMachine();
        Pos = pos;
        Speed  = speed;
        Health = 100;
    }
    public int Health { get; set; }
    public float Speed { get; set; }
    public Vector2 Pos { get; set; }
    public void MoveUp()
    {
        Pos = new Vector2(Pos.X, Pos.Y - Speed);
    }
    public void MoveDown()
    {
        Pos = new Vector2(Pos.X, Pos.Y + Speed);
    }
    public void MoveRight()
    {
        Pos = new Vector2(Pos.X + Speed, Pos.Y);
    }
    public void MoveLeft()
    {
        Pos = new Vector2(Pos.X - Speed, Pos.Y);
    }
}
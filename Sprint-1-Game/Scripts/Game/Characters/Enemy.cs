using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Enemy : ICharacter
{
    public Enemy(Vector2 pos, float speed)
    {
        Pos = pos;
        Speed  = speed;
        Health = 100;
    }
    public int Health { get; set; }
    public float Speed { get; set; }
    public Vector2 Pos { get; set; }
}
using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Enemy : Character
{
    public Enemy(Vector2 pos, float speed, Direction direction)
        : base(pos, speed, direction)
    {
    }
}
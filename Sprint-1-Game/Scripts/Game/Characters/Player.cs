using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Player : Character
{
    public Player(Vector2 pos, float speed, Direction direction)
        : base(pos, speed, direction)
    {
    }
}
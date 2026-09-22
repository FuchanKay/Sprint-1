using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;
/*Basic functions for characters like players, enemies, and so on*/
public interface ICharacter
{
    public int Health { get; set; }
    public float Speed { get; set; }
    public Vector2 Pos { get; set; }

}
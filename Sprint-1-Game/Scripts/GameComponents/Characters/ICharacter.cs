using Microsoft.Xna.Framework;
public interface ICharacter
{
    public float Speed { get; set; }
    public Vector2 Pos { get; set; }
    public void MoveNorth();
    public void MoveEast();
    public void MoveSouth();
    public void MoveWest();
}
using Scripts.Game;
using Scripts.GameComponents;

public interface IObject
{
    ObjectIds Id { get; }
    Directions Direction { get; set; }
}
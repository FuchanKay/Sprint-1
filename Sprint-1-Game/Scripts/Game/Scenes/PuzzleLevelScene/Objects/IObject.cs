namespace Scripts.Game;

public interface IObject
{
    ObjectIds Id { get; }
    Directions Direction { get; set; }
}
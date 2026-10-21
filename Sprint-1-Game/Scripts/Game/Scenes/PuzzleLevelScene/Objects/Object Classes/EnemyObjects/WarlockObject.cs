namespace Scripts.Game;

/*Warlock (Easy): When the player snaps, the player and the warlock will swap positions. Warlock does not attack. Pushable?
Idle
Hands raised to do magic?
*/
public class WarlockObject : IObject
{
    public IObject Clone()
    {
        return new WarlockObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Warlock;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;


}
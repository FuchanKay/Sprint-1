namespace Scripts.Game;

/*Blue Lizards (Hard): Blue lizards will shoot a beam of fire in the direction it is facing when the player snaps. Pushable. 
Up/Down/Left/Right
Shooting fire
Dead (squished?)
*/
public class BlueLizard : IEnemy
{
    public IObject Clone()
    {
        return new BlueLizard
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.BlueLizard;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public int Health { get; set; } = 1;
}
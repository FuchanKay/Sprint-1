namespace Scripts.Game;

/*Red Lizards (Hard): Red lizards will shoot a beam of fire in the direction it is facing when the player walks into line of sight. Pushable. 
Up/Down/Left/Right
Shooting fire
Dead (squished?)
*/

public class RedLizardObject : IObject
{
    public IObject Clone()
    {
        return new RedLizardObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.RedLizard;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => true;

}
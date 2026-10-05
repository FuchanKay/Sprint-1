namespace Scripts.Game;

/*Red Lizards (Hard): Red lizards will shoot a beam of fire in the direction it is facing when the player walks into line of sight. Pushable. 
Up/Down/Left/Right
Shooting fire
Dead (squished?)
*/

public class RedLizardObject : IObject
{
    public ObjectIds Id => ObjectIds.Enemy;
    public Directions Direction { get; set; } = Directions.South;
    public bool isPushable => true;
    public bool isDestructible => true;

}
namespace Scripts.Game;

/*Blue Lizards (Hard): Blue lizards will shoot a beam of fire in the direction it is facing when the player snaps. Pushable. 
Up/Down/Left/Right
Shooting fire
Dead (squished?)
*/
public class BlueLizardObject : IObject
{
    public ObjectIds Id => ObjectIds.BlueLizard;
    public Directions Direction { get; set; } = Directions.South;
    public bool isPushable => true;
    public bool isDestructible => true;

}
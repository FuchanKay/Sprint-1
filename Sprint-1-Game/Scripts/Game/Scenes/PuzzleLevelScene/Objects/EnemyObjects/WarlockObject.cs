namespace Scripts.Game;

/*Warlock (Easy): When the player snaps, the player and the warlock will swap positions. Warlock does not attack. Pushable?
Idle
Hands raised to do magic?
*/
public class WarlockObject : IObject
{
    public ObjectIds Id => ObjectIds.Empty;
    public Directions Direction { get; set; }

}
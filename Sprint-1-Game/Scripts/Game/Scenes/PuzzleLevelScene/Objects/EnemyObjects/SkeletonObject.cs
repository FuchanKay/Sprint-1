namespace Scripts.Game;

/*Skeletons (Medium): Skeletons will automatically attack the player if they are within a 1 grid radius. 
Up/Down/Left/Right
Attacking
Dead 
*/
public class SkeletonObject : IObject
{
    public ObjectIds Id => ObjectIds.Enemy;
    public Directions Direction { get; set; } = Directions.South;

}
namespace Scripts.Game;

/*Skeletons (Medium): Skeletons will automatically attack the player if they are within a 1 grid radius. 
Up/Down/Left/Right
Attacking
Dead 
*/
public class SkeletonObject : IObject
{
    public IObject Clone()
    {
        return new SkeletonObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Skeleton;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;


}
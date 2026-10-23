using Scripts.GameComponents;
namespace Scripts.Game;
public class PlayerItemsComponent
{
        public static IObject[] playerItems= [new EmptyObject(), new EmptyObject(), new EmptyObject()]; 
        public static ISprite[] playerItemSprites = new ISprite[3];

        public static Coordinate aquireTarget(Coordinate playerCoord, Directions playerDirection)
        {
                Coordinate targetCoord = new Coordinate();
                switch (playerDirection)
                {
                        case Directions.North:
                        targetCoord = playerCoord + Coordinate.North;
                        break;
                        case Directions.East:
                        targetCoord = playerCoord + Coordinate.East;
                        break;
                        case Directions.South:
                        targetCoord = playerCoord + Coordinate.South;
                        break;
                        case Directions.West:
                        targetCoord = playerCoord + Coordinate.West;
                        break;
                }
                return targetCoord;
 
        }
        
}
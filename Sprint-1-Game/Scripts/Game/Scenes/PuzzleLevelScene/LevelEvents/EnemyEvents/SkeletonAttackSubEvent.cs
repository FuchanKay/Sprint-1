using System;
using System.Collections.Generic;
using System.Data;

namespace Scripts.Game;

public class SkeletonAttackSubEvent(Coordinate skeletonCoord, ObjectFactory objectFactory, SubEventCallStack subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        List<Directions> directions = new List<Directions> {Directions.North, Directions.East, Directions.South, Directions.West};

        foreach(var direction in directions)
        {
            var targetCoord = skeletonCoord + Coordinate.ToCoordinate(direction);
            var targetObj = gridPointer.GetObject(targetCoord);
            if(targetObj.Id == ObjectIds.Player)
            {
                // Change skeleton facing direction and sprite
                // Play attack animation
                Console.WriteLine("Skeleton Attack!");
                subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
            }
        }

        context.IsIdle = false;
    }
}
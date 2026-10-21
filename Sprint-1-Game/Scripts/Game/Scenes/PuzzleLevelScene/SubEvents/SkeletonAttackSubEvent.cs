using System;
using System.Collections.Generic;

namespace Scripts.Game;

public class SkeletonAttackSubEvent(Coordinate skeletonCoord, ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
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
                // TODO: Change skeleton facing direction and sprite
                spriteGrid.PlayEffect(EffectIds.Slash, targetCoord);
                Console.WriteLine($"Skeleton Attacking {direction}!");
                subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
            }
        }

        

        context.IsIdle = false;
    }
}
using System;
using System.Collections.Generic;

namespace Scripts.Game;

public class SkeletonsAttackSubEvent(List<Coordinate> skeletonCoords, ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        List<Directions> directions = new List<Directions> {Directions.North, Directions.East, Directions.South, Directions.West};

        var attackOccurred = false;
        foreach(var skeletonCoord in skeletonCoords)
        {
            if (TryAttack(skeletonCoord, gridPointer, spriteGrid))
            {
                attackOccurred = true;
            }
        }
        context.IsIdle = attackOccurred;
    }

    private bool TryAttack(Coordinate skeletonCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        List<Directions> directions = new List<Directions> {Directions.North, Directions.East, Directions.South, Directions.West};   
        var attackOccurred = false;
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
                attackOccurred = true;
            }
        }
        return attackOccurred;
    }
}   
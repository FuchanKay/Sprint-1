using System.Collections.Generic;
using System.Linq;
namespace Scripts.Game;

public class BlueLizardAttackSubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public virtual void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var lizardCoords = gridPointer.GetObjectPointers(ObjectIds.BlueLizard).Keys.ToList();
        foreach(var lizardCoord in lizardCoords)
        {
            FireBlastAttack(lizardCoord, gridPointer, spriteGrid);
        }
    }
    private void FireBlastAttack(Coordinate enemyCoord, GridPointer gridPointer, SpriteGrid spriteGrid)
    {
        var enemy = gridPointer.GetObject(enemyCoord);
        var direction = enemy.Direction;
        Stack<Coordinate> lineOfSight = GetLineOfSight(gridPointer, enemyCoord, direction);
        bool seesPlayer = gridPointer.GetObject(lineOfSight.Peek()).Id == ObjectIds.Player;
        bool seesDestructible = gridPointer.GetObject(lineOfSight.Peek()).IsDestructible;
        bool seesEnemy = gridPointer.GetObject(lineOfSight.Peek()) is IEnemy;
        if(gridPointer.GetObject(lineOfSight.Peek()).Id == ObjectIds.Wall) lineOfSight.Pop();

        foreach (var coord in lineOfSight)
        {
            spriteGrid.PlayEffect(EffectIds.BlueFire, coord);
        }

        if (seesPlayer) subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        else if (seesEnemy) subEventStack.Push(new EnemyDamageSubEvent(objectFactory, lineOfSight.Peek()));
        else if (seesDestructible)
        {
            gridPointer.SetObject(lineOfSight.Peek(), ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(lineOfSight.Peek());
        }
    }

    private static Stack<Coordinate> GetLineOfSight(GridPointer gridPointer, Coordinate enemyCoord, Directions direction)
    {
        Stack<Coordinate> lineOfSight = [];
        var nextCoord = enemyCoord + Coordinate.ToCoordinate(direction);
        lineOfSight.Push(nextCoord);

        while(gridPointer.GetObject(nextCoord).Id == ObjectIds.Empty)
        {
            nextCoord += Coordinate.ToCoordinate(direction);
            lineOfSight.Push(nextCoord);
        }

        return lineOfSight;
    }
}
using System.Collections.Generic;
namespace Scripts.Game;

public class PickItemEvent(ObjectFactory objectFactory, TileFactory tileFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory, tileFactory));
        SubEventStack.Push(new PickItemSubEvent(objectFactory));

    }
}
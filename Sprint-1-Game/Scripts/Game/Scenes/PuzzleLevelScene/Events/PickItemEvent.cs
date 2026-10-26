using System.Collections.Generic;
namespace Scripts.Game;

public class PickItemEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory));
        SubEventStack.Push(new PickItemSubEvent(objectFactory));

    }
}
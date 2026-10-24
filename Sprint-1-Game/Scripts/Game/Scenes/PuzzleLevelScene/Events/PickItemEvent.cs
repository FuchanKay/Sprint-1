using System.Collections.Generic;
namespace Scripts.Game;
using System;

public class PickItemEvent(ObjectFactory objectFactory, InventoryManager inventoryManager) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        SubEventStack.Push(new PickItemSubEvent(objectFactory, inventoryManager));

    }
}
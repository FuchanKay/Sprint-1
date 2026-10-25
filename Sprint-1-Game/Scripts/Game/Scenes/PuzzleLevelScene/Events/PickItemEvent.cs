using System.Collections.Generic;
namespace Scripts.Game;
using System;

public class PickItemEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        Console.WriteLine( "pick event");

        SubEventStack.Push(new PickItemSubEvent(objectFactory));

    }
}
using System;
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent(Inventory inventory, ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        SubEventStack.Push(new UseItemSubEvent(inventory, objectFactory));
    }
}
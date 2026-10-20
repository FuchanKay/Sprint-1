using System.Collections.Generic;

namespace Scripts.Game;

public class UndoEvent : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {

    }
}
using System.Collections.Generic;

namespace Scripts.Game;

public interface ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; }
    void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context);
}
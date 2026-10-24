using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEventEvent : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, List<GridPointer> undoHistory)
    {

    }
}
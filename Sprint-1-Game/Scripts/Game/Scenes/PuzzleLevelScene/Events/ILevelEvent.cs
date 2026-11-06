using System.Collections.Generic;
using Scripts.GameComponents;

namespace Scripts.Game;

public interface ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; }
    void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager, List<LevelStateHistory> undoHistory);
}
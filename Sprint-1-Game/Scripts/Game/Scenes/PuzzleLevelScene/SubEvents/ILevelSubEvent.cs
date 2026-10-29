using Scripts.GameComponents;

namespace Scripts.Game;

public interface ILevelSubEvent
{
    void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager);
}
namespace Scripts.Game;
using Scripts.GameComponents;

public class PlayerDeathSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        spriteGrid.PlayEffect(EffectIds.PlayerDeath, playerCoord);
        spriteGrid.RemoveObjectSprite(playerCoord);
        gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
        context.PlayerIsDead = true;
    }
}
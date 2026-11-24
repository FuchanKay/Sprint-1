namespace Scripts.Game;

using Scripts.GameComponents;

public class PlayerDeathSubEvent(EffectIds effectId) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        spriteGrid.PlayEffect(effectId, playerCoord);
        audioManager.PlaySound(SoundNames.Death);
        spriteGrid.RemoveObjectSprite(playerCoord);
        gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
        context.PlayerIsDead = true;
    }
}
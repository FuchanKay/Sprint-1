namespace Scripts.Game;

public class PlayerDeathSubEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        spriteGrid.PlayEffect(EffectIds.PlayerDeath, playerCoord);
        spriteGrid.RemoveObjectSprite(playerCoord);
        gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
        context.PlayerIsDead = true;
    }
}
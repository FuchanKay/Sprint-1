namespace Scripts.Game;

public class PlayerDeathSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        spriteGrid.PlayEffect(EffectIds.PlayerDeath, playerCoord);
        spriteGrid.RemoveObjectSprite(playerCoord);
        gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
        context.PlayerIsDead = true;
    }
}
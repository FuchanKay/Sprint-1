namespace Scripts.Game;

public partial class PlayerDeathSubEvent : ILevelSubEvent
{
    private readonly ObjectFactory objectFactory;

    public PlayerDeathSubEvent(ObjectFactory objectFactory)
    {
        this.objectFactory = objectFactory;
    }

    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        spriteGrid.PlayEffect(EffectIds.PlayerDeath, playerCoord);
        spriteGrid.RemoveObjectSprite(playerCoord);
        gridPointer.SetObject(playerCoord, objectFactory.CreateEmpty());
        context.PlayerIsDead = true;
    }
}
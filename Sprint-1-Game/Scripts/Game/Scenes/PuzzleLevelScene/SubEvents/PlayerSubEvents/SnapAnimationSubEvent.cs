using Scripts.GameComponents;

namespace Scripts.Game;

public class SnapAnimationSubEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerCoord);
        audioManager.PlaySound(SoundNames.Snap);
    }
}
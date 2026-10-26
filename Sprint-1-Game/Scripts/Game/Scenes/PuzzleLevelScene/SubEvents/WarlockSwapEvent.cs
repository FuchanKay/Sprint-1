using System.Linq;
using System;

namespace Scripts.Game;

public class WarlockSwapEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var warlockPointer = gridPointer.GetObjectPointers(ObjectIds.Warlock);
        bool warlockExists = warlockPointer.Count > 0;

        if (warlockExists)
        {
            var warLockCoord = warlockPointer.Keys.First();
            var warLockObj = gridPointer.GetObject(warLockCoord);
            var playerCoord = gridPointer.GetPlayerCoord();
            var playerObj = gridPointer.GetObject(playerCoord);

            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            var warlockSprite = spriteGrid.GetObjectSprite(warLockCoord);

            gridPointer.SetObject(warLockCoord, playerObj);
            gridPointer.SetObject(playerCoord, warLockObj);

            spriteGrid.RemoveObjectSprite(playerCoord);
            spriteGrid.RemoveObjectSprite(warLockCoord);
            spriteGrid.PlayEffect(EffectIds.WarlockSpell, warLockCoord);
            spriteGrid.PlayEffect(EffectIds.WarlockSpell, playerCoord);
            playerSprite.CurrentPosition = spriteGrid.CalculateLiteralPos(warLockCoord);
            warlockSprite.CurrentPosition = spriteGrid.CalculateLiteralPos(playerCoord);
            spriteGrid.SetObjectSprite(warLockCoord, warLockCoord, playerSprite);
            spriteGrid.SetObjectSprite(playerCoord, playerCoord, warlockSprite);
        }
    }
}
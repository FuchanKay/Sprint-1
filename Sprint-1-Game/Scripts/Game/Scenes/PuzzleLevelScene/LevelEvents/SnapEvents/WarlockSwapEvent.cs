using System.Linq;
using System;

namespace Scripts.Game;

public class WarlockSwapEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
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

            var testObj = gridPointer.GetObject(warLockCoord);
            Console.WriteLine($"testObj.Idf is {testObj.Id}");
            var testObj2 = gridPointer.GetObject(playerCoord);
            Console.WriteLine($"testObj2.Idf is {testObj2.Id}");

            spriteGrid.RemoveObjectSprite(playerCoord);
            spriteGrid.RemoveObjectSprite(warLockCoord);
            playerSprite.CurrentPosition = spriteGrid.CalculateLiteralPos(warLockCoord);
            warlockSprite.CurrentPosition = spriteGrid.CalculateLiteralPos(playerCoord);
            spriteGrid.SetObjectSprite(warLockCoord, warLockCoord, playerSprite);
            spriteGrid.SetObjectSprite(playerCoord, playerCoord, warlockSprite);
        }
    }
}
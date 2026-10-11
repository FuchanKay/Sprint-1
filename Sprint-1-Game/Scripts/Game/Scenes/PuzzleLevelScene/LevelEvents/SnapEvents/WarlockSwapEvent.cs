using System;
using System.Linq;
using Scripts.Game;

public class WarlockSwapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var WarlockPointer = gridPointer.GetObjectPointers(ObjectIds.Warlock);
        var WarlockCoords = WarlockPointer.Keys.ToList();
        var PlayerCood = gridPointer.GetPlayerCoord();
        foreach (var WarlockCoord in WarlockCoords){
            var tempCoord = PlayerCood;
            PlayerCood = WarlockCoord;

        }

        
        context.ShouldUpdate = true;

    }
}
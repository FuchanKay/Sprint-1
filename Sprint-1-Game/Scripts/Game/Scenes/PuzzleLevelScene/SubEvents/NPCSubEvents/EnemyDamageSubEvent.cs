using System.Linq;
using Scripts.GameComponents;
namespace Scripts.Game;

public class EnemyDamageSubEvent(ObjectFactory objectFactory, Coordinate enemyCoord) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var enemy = gridPointer.EnemyCollection[enemyCoord];
        enemy.Health--;
        if(enemy.Health == 0) KillEnemy(spriteGrid, gridPointer);
    }

    private void KillEnemy(SpriteGrid spriteGrid, GridPointer gridPointer)
    {
        spriteGrid.PlayEffect(EffectIds.EnemyDeath, enemyCoord);
        var doorPointers = gridPointer.GetObjectPointers(ObjectIds.Door);
        var door = doorPointers.First().Value as DoorObject;
        bool closedDoorExists = doorPointers.Count > 0 && !door.IsOpen;
        if (gridPointer.EnemyCollection.Count == 1 && closedDoorExists) 
        {
            var obj = ObjectFactory.CreateKey();
            gridPointer.SetObject(enemyCoord, obj);
            spriteGrid.SetNewObjectSprite(enemyCoord, obj);
        }    
        else 
        {
            gridPointer.SetObject(enemyCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(enemyCoord); 
        }
    }
}
namespace Scripts.Game;

public class EnemyDamageSubEvent(ObjectFactory objectFactory, Coordinate enemyCoord) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var enemy = gridPointer.EnemyCollection[enemyCoord];
        enemy.Health--;
        if(enemy.Health == 0) KillEnemy(spriteGrid, gridPointer);
    }

    private void KillEnemy(SpriteGrid spriteGrid, GridPointer gridPointer)
    {
        spriteGrid.PlayEffect(EffectIds.EnemyDeath, enemyCoord);
        if (gridPointer.EnemyCollection.Count == 1) 
        {
            var obj = objectFactory.CreateKey();
            gridPointer.SetObject(enemyCoord, obj);
            spriteGrid.PlaceNewObjectSprite(enemyCoord, obj);
        }    
        else 
        {
            gridPointer.SetObject(enemyCoord, objectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(enemyCoord); 
        }
    }
}
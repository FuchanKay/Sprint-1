namespace Scripts.Game;

public class EnemyDamageSubEvent(ObjectFactory objectFactory, Coordinate enemyCoord) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var enemy = gridPointer.EnemyCollection[enemyCoord];
        enemy.Health--;
        if(enemy.Health == 0) KillEnemy(spriteGrid, gridPointer);
    }

    private void KillEnemy(SpriteGrid spriteGrid, GridPointer gridPointer)
    {
        spriteGrid.PlayEffect(EffectIds.EnemyDeath, enemyCoord);
        spriteGrid.RemoveObjectSprite(enemyCoord);
        if (gridPointer.EnemyCollection.Count == 1) 
        {
            gridPointer.SetObject(enemyCoord, objectFactory.CreateEmpty()); // Will be a key object
        }    
        else 
        {
            gridPointer.SetObject(enemyCoord, objectFactory.CreateEmpty()); 
        }
    }
}
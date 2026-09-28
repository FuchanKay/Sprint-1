using System;
using Scripts.Game;

public class RandomMove
{
    private Enemy Enemy;
    public RandomMove(Enemy enemy)
    {
        Enemy = enemy;
        Random random = new Random();
        int[] numbers = [0,0,0,0,0,1,2,3,4];
        int stateIndex = numbers[random.Next(0, numbers.Length)];
        switch (stateIndex)
        {
            case 0:

            break;

            case 1:
                enemy.MoveNorth();
            break;

            case 2:
                enemy.MoveEast();
            break;

            case 3:
                enemy.MoveSouth();
            break;

            case 4:
                enemy.MoveWest();
            break;
        }
        
    }


}
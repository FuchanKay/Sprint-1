// using Microsoft.Xna.Framework;
// using System.Collections.Generic;

// namespace Scripts.Game;

// public class RedLizardBlastSubEvent(ObjectFactory objectFactory, List<LevelStateHistory> levelStateHistory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
// {
//     public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
//     {
//         var redLizards = gridPointer.GetObjectPointers(ObjectIds.RedLizard);

//         foreach (var lizard in redLizards)
//         {
//             var lizardCoord = lizard.Key;
//             var lizardDirection = lizard.Value.Direction;
//             var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);

//             var furthestObjectCoord = Pathfinder.GetFurthestNonWallObjectCoord(gridPointer, lizardCoord, lizardDirection);
//             var currObjectAhead = gridPointer.GetObject(furthestObjectCoord);
//             var nonWallObjectAhead = currObjectAhead.Id != ObjectIds.Wall;

//             var prev = levelStateHistory.Count - 1;
//             var prevGridPointer = levelStateHistory[prev].GridPointer;
//             var prevCoord = Pathfinder.GetFurthestNonWallObjectCoord(prevGridPointer, lizardCoord, lizardDirection);
//             var prevObject = prevGridPointer.GetObject(furthestObjectCoord);
//             var isNewObject = prevObject.Id != currObjectAhead.Id || prevCoord.X != furthestObjectCoord.X || prevCoord.Y != furthestObjectCoord.Y;

//             var isIdle = lizardSprite.IdleName.Contains("Idle");

//             if (nonWallObjectAhead && (isIdle || isNewObject))
//             {
//                 LaserBlast(spriteGrid, gridPointer, lizardCoord, furthestObjectCoord, lizardDirection);
//             }
//         }
//     }

//     private void LaserBlast(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate startCoord, Coordinate endCoord, Directions lizardDirection)
//     {
//         var lizardSprite = spriteGrid.GetObjectSprite(startCoord);
//         var mouthOpen = RegionNames.ToLizardMouthOpenName(lizardDirection);
//         lizardSprite.SetState(mouthOpen);
//         lizardSprite.IdleName = mouthOpen;

//         // N/S -> rotation = 0
//         // E/W -> rotation = 90
//         var rotation = MathHelper.ToRadians(((int)lizardDirection % 2) * 90);

//         var dirCoord = startCoord + Coordinate.ToCoordinate(lizardDirection);
//         var startDirCoord = dirCoord;
//         var dirGrid = gridPointer.GetGrid(dirCoord);
//         while (dirCoord.X != endCoord.X || dirCoord.Y != endCoord.Y)
//         {
//             spriteGrid.AddIterableEffectSprite(EffectIds.LaserBeam, startDirCoord, dirCoord, rotation);

//             if(dirGrid.Object.Id == ObjectIds.Player)
//             {
//                 subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
//             } else
//             {
//                 gridPointer.SetObject(dirCoord, objectFactory.CreateEmpty());
//                 spriteGrid.RemoveObjectSprite(dirCoord);
//             }

//             dirCoord += Coordinate.ToCoordinate(lizardDirection);
//             dirGrid = gridPointer.GetGrid(dirCoord);
//         }

//         if(dirGrid.Object.IsDestructible)
//         {
//             gridPointer.SetObject(dirCoord, objectFactory.CreateEmpty());
//             spriteGrid.RemoveObjectSprite(dirCoord);
//         }

//         if(dirGrid.Object.Id == ObjectIds.Player)
//         {
//             subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
//         }

//         spriteGrid.AddIterableEffectSprite(EffectIds.PlayerDeath, startDirCoord, dirCoord, 0);

//         spriteGrid.StartIterableEffect(startDirCoord);
//     }
// }
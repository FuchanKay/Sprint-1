# Sprint-1 Game

*Name Pending* is a grid-based puzzle game built with C# and MonoGame. Move through the level, push objects, and use the special tiles and objects to solve the room.

## Controls

W, A, S, D or arrow keys => Face and move in that direction
Left Shift + direction => Turn to face that direction without moving
F => Snap: activate bombs, rotate bug tiles, and trigger the other snap interactions
Z => Undo the previous move
G => Pickup pickable items
1, 2, 3 => Use items in each inventory slot (Inventory HUD shown in the console for now)
B => Change Level
R => Restart the current level (can be undone)
Ctrl + R => Restart the entire game
Q => Exit the game

## Level Elements

### Tiles

Dirt, grass, brick, mossy brick => Walkable floor; these are visual variations of the same floor behavior.
Water => Walkable. It has no additional gameplay effect at present.
Pit => Blocks movement.
Lava => Walkable, but dangerous: after a move, the player is defeated if standing on lava. Other objects on lava are destroyed during the level check.
Clockwise bug tile => Walkable. When you snap, it rotates rotatable objects on its tile clockwise.
Counterclockwise bug tile => Walkable. When you snap, it rotates rotatable objects on its tile counterclockwise.

### Objects, Enemies, and Characters

Sprout => The character you control.
Wall => Blocks movement and cannot be pushed or destroyed.
Rock => Pushable and destructible. Push it into an empty tile; bombs can destroy it.
Bomb => Pushable and pickable. Snapping detonates it, affecting a 3×3 area centered on the bomb. Destructible objects in that area are removed, and both players and enemies are defeated if caught in the blast.
Timed bomb => Pushable. Its countdown advances when you snap; it detonates when its remaining count reaches one. Its blast follows the same area and destruction rules as a bomb.
Key => Unlocks a closed door present in the level. Dropped once all enemies are defeated.
Door => When open, acts as the exit to the current screen.

### Enemies

Skeleton => Stationary. It can attack the player in all cardinal directions; getting hit defeats the player.
Warlock => Pushable and stationary. When you snap, it swaps positions with the player.
Red lizards => Pushable and stationary. Shoots a ranged fire attack at the player whenever the player enters its line of sight.
Blue lizards => Pushable and stationary. Shoots a ranged fire attack when the player snaps. This attack can be used by the player to defeat enemies and destroy destructible objects.
(Both lizards will be rotatable in the future, but currently are not)
Frog => Moves randomly in a cardinal direction as the player moves. Has no attack behavior.
Zombie => Moves in a fixed movement pattern (Currently a horizontal line). It attacks the player if they are standing in the direction it is facing.
Bat => Pathfinds to the player and moves towards their current position. It can attack the player in all cardinal directions.

## Project

The solution is in `Sprint-1-Game.sln`. The game project targets .NET 9 and uses MonoGame.

Repository: <https://github.com/FuchanKay/Sprint-1>

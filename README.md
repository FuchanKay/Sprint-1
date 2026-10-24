# Sprint-1 Game

*Name Pending* is a grid-based puzzle game built with C# and MonoGame. Move through the level, push objects, and use the special tiles and objects to solve the room.

## Controls

W, A, S, D or arrow keys => Face and move in that direction
Left Shift + direction => Turn to face that direction without moving
F => Snap: activate bombs, rotate bug tiles, and trigger the other snap interactions
Z => Undo the previous move
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

Player => The character you control.
Rock => Pushable and destructible. Push it into an empty tile; bombs can destroy it.
Bomb => Pushable. Snapping detonates it, affecting a 3×3 area centered on the bomb. Destructible objects in that area are removed, and the player is defeated if caught in the blast.
Timed bomb => Pushable. Its countdown advances when you snap; it detonates when its remaining count reaches one. Its blast follows the same area and destruction rules as a bomb.
Wall => Blocks movement and cannot be pushed or destroyed.
Skeleton => Pushable and destructible. It attacks when adjacent to the player; getting hit defeats the player.
Warlock => Pushable and destructible. When you snap, it swaps positions with the player.
Blue and red lizards => Present as pushable, destructible objects. Their attack behaviors are not implemented yet.

## Project

The solution is in `Sprint-1-Game.sln`. The game project targets .NET 9 and uses MonoGame.

Repository: <https://github.com/FuchanKay/Sprint-1>

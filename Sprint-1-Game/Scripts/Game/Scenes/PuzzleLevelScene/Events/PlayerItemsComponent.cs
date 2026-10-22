using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;
public class PlayerItemsComponent
{
        public static IObject[] playerItems= [new EmptyObject(), new EmptyObject(), new EmptyObject()]; 
        public static ISprite[] playerItemSprites = new ISprite[3];

}
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;


namespace GUMProject.Setups
{
    public class InventorySetup
    {
        private readonly Dictionary<string, Vector2> ItemSpriteDefinitions = new();
        public InventorySetup()
        {
            SetupItemIconPositions();
        }

        private void SetupItemIconPositions()
        {
            AddItemDef("VSeed", new Vector2(0, 64));
            AddItemDef("PSeed", new Vector2(64 * 1, 64));
            AddItemDef("Bomb", new Vector2(64 * 2, 64));
            AddItemDef("Empty", new Vector2(64 * 3, 64));
        }
        private void AddItemDef(string name, Vector2 topLeft)
        {
            ItemSpriteDefinitions.Add(name, topLeft);
        }
        public Vector2 GetItemDef(string name)
        {
            return ItemSpriteDefinitions[name];
        }
    }
}
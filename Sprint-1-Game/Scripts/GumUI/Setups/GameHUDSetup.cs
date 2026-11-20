using HytaleHotbar.Components.Hytale;
using HytaleHotbar.Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;


namespace HytaleHotbar.Services
{
    public class InventoryService
    {
        Dictionary<string, Vector2> ItemSpriteDefinitions;
        public IReadOnlyDictionary<string, Vector2> ItemSpriteDefinitions => Vector2;
        public InventoryService()
        {
            SetupItemIconPositions();
        }

        private void SetupItemIconPositions()
        {
            AddItemIcon("VSeed", new Vector2(0, 64));
            AddItemIcon("PSeed", new Vector2(64 * 1, 64));
            AddItemIcon("Bomb", new Vector2(64 * 2, 64));
            AddItemIcon("Empty", new Vector2(64 * 3, 64));
        }
        private void AddItemIcon(string name, Vector2 topLeft)
        {
            _itemDefinitions.Add(name, topLeft);
        }
        public bool ValidIndex(int index)
        {
            if (index < 0 || index >= MaxItemSlots)
            {
                return false;
            }
            return true;
        }
    }
}
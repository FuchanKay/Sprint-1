using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using RenderingLibrary.Graphics;

using System.Linq;

namespace GUMProject.Components.GameUI
{
    partial class InventorySlot
    {
        public event System.EventHandler Click;
        partial void CustomInitialize()
        {
            this.Visual.Click += (_, args) => Click?.Invoke(this, args);
        }
        public void SetSlotToItem(string item, Vector2 itemSpriteDef)
        {
            if (item == null)
            {
                this.HasItemState = HasItem.False;
                return;
            }
            this.HasItemState = HasItem.True;
            this.IconLeft = (int)itemSpriteDef.X;
            this.IconTop = (int)itemSpriteDef.Y;
        }
    }
}

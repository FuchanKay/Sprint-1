
using Microsoft.Xna.Framework;


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

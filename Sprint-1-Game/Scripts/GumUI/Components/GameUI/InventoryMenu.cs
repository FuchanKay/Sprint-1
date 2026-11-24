using Gum;
using Gum.ExtensionMethods;
using Gum.Forms.Input;
using System;


namespace GUMProject.Components.GameUI
{
    partial class InventoryMenu
    {
        public InventorySlot Slot(int i) => (InventorySlot)InventoryPanel.Children[i];
        private int CurrentSelectedIndex = -1;
        public int SelectedIndex
        {
            get => CurrentSelectedIndex;
            set
            {   if(CurrentSelectedIndex != value)
                {
                    if(CurrentSelectedIndex != -1)
                    {
                        Slot(CurrentSelectedIndex).Selected = false;
                    }
                    CurrentSelectedIndex = value;
                    Slot(CurrentSelectedIndex).Selected = true;
                }
            }
        }
        partial void CustomInitialize()
        {
            foreach (InventorySlot child in InventoryPanel.Children)
            {
                child.Click += HandleItemSlotClicked;
            }
        }
        internal void HandleKeyboardInput()
        {
            var keyboard = GumService.Default.Keyboard;
            int? indexToSelect = null;
            if(keyboard.KeyPushed(Keys.D1)) indexToSelect = 0;
            if(keyboard.KeyPushed(Keys.D2)) indexToSelect = 1;
            if(keyboard.KeyPushed(Keys.D3)) indexToSelect = 2;

            if(indexToSelect != null)
            {
                SelectedIndex = indexToSelect.Value;
            }
        }
        private void HandleItemSlotClicked(object sender, EventArgs e)
        {
            InventorySlot inventorySlot = (InventorySlot)sender;
            SelectedIndex = InventoryPanel.Children.IndexOf(inventorySlot);
        }
       
    }
}

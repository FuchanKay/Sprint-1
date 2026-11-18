using Gum.Converters;
using Gum.DataTypes;
using Gum.Forms.Controls;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;
using System;
using System.Diagnostics;
using System.Linq;

namespace GUMProject.Components.GameUI
{
    partial class InventoryBar
    {
        partial void CustomInitialize()
        {
            foreach (FrameworkElement child in InventoryBarPanel.Children)
            {
                child.Visual.Click += HandleItemSlotClicked;
            }
        }

        private void HandleItemSlotClicked(object sender, EventArgs e)
        {
            var visual = sender as InteractiveGue;
            var inventorySlot = visual.FormsControlAsObject as InventorySlot;
            inventorySlot.Selected = true;
            Debug.WriteLine($"Clicked {inventorySlot.Name}");
        }
    }
}

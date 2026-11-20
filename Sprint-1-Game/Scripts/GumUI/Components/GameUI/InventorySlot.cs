using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

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
    }
}

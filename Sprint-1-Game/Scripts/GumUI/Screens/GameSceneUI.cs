using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Screens;
using Microsoft.Xna.Framework;
using RenderingLibrary.Graphics;
using System;
using System.Linq;

namespace GUMProject.Screens
{
    partial class GameSceneUI : IUpdateScreen
    {
        private int Clock = 0;
        partial void CustomInitialize()
        {
                Clock = 0;
        }
        public void Update(int dtMs)
        {
            GameSceneHUDInstance.InventoryMenuInstance.HandleKeyboardInput();
            Clock += dtMs;
            TimeSpan formattedClock = TimeSpan.FromMilliseconds(Clock);
            GameSceneHUDInstance.TimeMenuInstance.TimeText = $"{formattedClock:hh\\:mm\\:ss}";
        }
    }
}

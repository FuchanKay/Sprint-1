using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GUMProject.Components.GameUI;
using GUMProject.Components.GameUI.Pieces;
using GUMProject.Screens;
using GUMProject.Setups;
using Microsoft.Xna.Framework;
using RenderingLibrary.Graphics;
using Scripts.Game;
using System;
using System.Linq;

namespace GUMProject.Screens
{
    partial class GameSceneUI : IUpdateScreen, ILevelScreen
    {
        private int Clock = 0;
        private InventorySetup InventorySetup;
        private PuzzleLevel PuzzleLevel;
        partial void CustomInitialize()
        {
            InventorySetup = new();
                Clock = 0;
        }
        public void FetchLevel(PuzzleLevel puzzleLevel)
        {
            PuzzleLevel = puzzleLevel;
        }
        private void SetSlotFromLevel()
        {
            InventoryManager currentInventoryData = PuzzleLevel.CurrentInventoryManager;

            for(int i = 0; i < 3; i++)
            {
                string item = InventoryManager.ObjectIdToString(currentInventoryData.PlayerItems[i].Id);
                GameSceneHUDInstance.InventoryMenuInstance.Slot(i).SetSlotToItem(item, InventorySetup.GetItemDef(item));
            }
            
        }
        public void Update(int dtMs)
        {
            SetSlotFromLevel();
            GameSceneHUDInstance.InventoryMenuInstance.HandleKeyboardInput();
            Clock += dtMs;
            TimeSpan formattedClock = TimeSpan.FromMilliseconds(Clock);
            GameSceneHUDInstance.TimeMenuInstance.TimeText = $"{formattedClock:hh\\:mm\\:ss}";
            GameSceneHUDInstance.EnemyCounterMenuInstance.EnemyCounterText = $"{PuzzleLevel.LevelStateHistory.GridPointer.EnemyCollection.Count}";

        }
    }
}

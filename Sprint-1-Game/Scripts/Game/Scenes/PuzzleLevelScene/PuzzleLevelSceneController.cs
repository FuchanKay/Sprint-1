using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class PuzzleLevelSceneController(ISceneManager sceneManager, IInputManager buttonInput, IAudioManager audioManager, ITextureAtlas textureAtlas, SpriteFontManager spriteFontManager, GameWindow window) : ISceneController
{
    private PuzzleLevel Level;
    private LevelFileParser LevelFileParser;
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    private readonly EffectSpriteFactory EffectSpriteFactory = new(textureAtlas);
    private GameLayoutManager GameLayoutManager = new();
    private UIManager UIManager = new(spriteFontManager);
    private readonly IAudioManager AudioManager = audioManager;
    private ILevelTransition LevelTransition;
    public int CurrentLevelIndex = 0;
    private BasicSpriteFontWriter DeathMessage = new(spriteFontManager);

    public void Init()
    {
        GameLayoutManager.Init(window);
        LevelFileParser = new LevelFileParser();
        Level = new PuzzleLevel(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory, LevelFileParser, AudioManager, GameLayoutManager);
        Level.Init(CurrentLevelIndex);
        UIManager.Init(Level);
        LevelTransition = new WipeTransition(Level);
        InitDeathMessage();        
    }

    public void Init(GridPointer grid, LevelContext level)
    {
        GameLayoutManager.Init(window);
        Level = new PuzzleLevel(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory, LevelFileParser, AudioManager, GameLayoutManager);
        Level.Init(grid, level);
        UIManager.Init(Level);
        InitDeathMessage();
    }

    private void InitDeathMessage()
    {
        string deathMessageText = "Press Z to Undo, or R to Restart";
        Vector2 messageSize = spriteFontManager.GetFont(FontNames.DungeonFont).MeasureString(deathMessageText);
        float LevelScreenRatio = 4f/5f;
        Vector2 DeathMessagePosition = new Vector2(window.ClientBounds.Width * (LevelScreenRatio / 2f) - messageSize.X/2f, window.ClientBounds.Height/2f + messageSize.Y/2f);
        DeathMessage.Init(FontNames.DungeonFont, DeathMessagePosition, deathMessageText, Color.DarkRed);
    }

    public void Update(int dtMs)
    {
        if (LevelTransition.IsActive)
        {
            LevelTransition.Update(dtMs);
            Level.Update(dtMs);
            return;
        } else if (sceneManager.SceneTransition.IsActive)
        {
            Level.Update(dtMs);
            return;
        }

        var lockMove = buttonInput.IsHeld(InputNames.LShift);
        if (buttonInput.IsPressed(InputNames.MoveNorth) || buttonInput.IsPressed(InputNames.MoveNorthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.North, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast) || buttonInput.IsPressed(InputNames.MoveEastAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.East, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth) || buttonInput.IsPressed(InputNames.MoveSouthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.South, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest) || buttonInput.IsPressed(InputNames.MoveWestAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.West, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.ChangeLevel) && !Level.Context.LevelEditorMode)
        {
            CurrentLevelIndex = (CurrentLevelIndex + 1) % FileNames.LevelFileNames.Length;
            Level.Init(CurrentLevelIndex);
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            Level.EnqueueEvent(new SnapEvent());
        }
        else if (buttonInput.IsPressed(InputNames.UseItem1))
        {
            Level.EnqueueEvent(new UseItemEvent(0));
        }
        else if (buttonInput.IsPressed(InputNames.UseItem2))
        {
            Level.EnqueueEvent(new UseItemEvent(1));

        }
        else if (buttonInput.IsPressed(InputNames.UseItem3))
        {
            Level.EnqueueEvent(new UseItemEvent(2));

        }
        else if (buttonInput.IsPressed(InputNames.PickItem))
        {
            Level.EnqueueEvent(new PickItemEvent());

        }
        else if (buttonInput.IsPressed(InputNames.ResetLevel))
        {
            Level.RestartLevel();
        }
        else if (buttonInput.IsPressed(InputNames.Undo))
        {
            Level.Undo();
        }
        else if (buttonInput.IsPressed(InputNames.ToggleObjectView))
        {
            Level.ToggleObjectHide();
        }

        Level.Update(dtMs);
        UIManager.Update(dtMs);

        // Exits to the main menu upon a levels completion
        if (Level.Context.LevelIsCompleted && Level.Context.LevelEditorMode)
        {
            sceneManager.Restart();
        }
        else if (Level.Context.LevelIsCompleted && !sceneManager.SceneTransition.IsActive)
        {
            CurrentLevelIndex += 1;
            if (CurrentLevelIndex >= FileNames.LevelFileNames.Length)
            {
                sceneManager.SceneTransition.StartSceneTransition(SceneNames.GameCredits);
            } else {
                LevelTransition.StartLevelTransition(CurrentLevelIndex);
            }
        }
    }
    public void Draw(SpriteBatch sb)
    {
        Level.Draw(sb);
        UIManager.Draw(sb);
        if(Level.Context.PlayerIsDead) DeathMessage.Draw(sb);
        if(LevelTransition.IsActive) LevelTransition.Draw(sb);
    }
}
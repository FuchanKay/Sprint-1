using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SceneManager(IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, ShaderManager shaderManager, ITextureAtlas textureAtlas, UIFontAtlas uIFontAtlas, GameWindow window) : ISceneManager
{
    private readonly Dictionary<string, ISceneController> NameSceneMap = [];
    private ISceneController CurrentScene;
    public bool ShouldExit { get; private set; }
    public bool ShouldRestart { get; private set; }
    public ISceneTransition SceneTransition { get; set; }
    private QuitConfirmationPopup QuitConfirmation;
    public enum GameStates {Play, Paused};
    public GameStates currentGameState = GameStates.Play;

    public void Init()
    {
        ShouldExit = false;
        SceneTransition = new FadeTransition(this);

        AddScenesToMap();

        CurrentScene = NameSceneMap[SceneNames.MainMenu];
        CurrentScene.Init();
        QuitConfirmation = new QuitConfirmationPopup(this, buttonInput, uIFontAtlas, window);
        QuitConfirmation.Init();
    }

    public void Restart()
    {
        NameSceneMap.Clear();

        Init();
    }

    public void Update(int dtMs)
    {
        UpdateInputs();

        QuitConfirmation.Update();
        //Later on in development exiting game should not be mapped to a key but decent temporary solution for now. 
        if (buttonInput.IsHeld(InputNames.LCtrl) && buttonInput.IsPressed(InputNames.ResetGame))
        {
            Restart();
        }

        if (buttonInput.IsPressed(InputNames.ExitGame))
        {
            if(CurrentScene is MainMenuSceneController || CurrentScene is GameCreditsSceneController) ExitGame();
            else {
                currentGameState = GameStates.Paused;
                QuitConfirmation.IsOnscreen = true;
            }
        }
        
        if (SceneTransition.IsActive) SceneTransition.Update(dtMs);
        if(currentGameState == GameStates.Play) CurrentScene.Update(dtMs);
    }
        

    public void Draw(SpriteBatch sb)
    {
        QuitConfirmation.Draw(sb);
        CurrentScene.Draw(sb);
        if (SceneTransition.IsActive) SceneTransition.Draw(sb);
    }

    public void SwapScene(string sceneName)
    {
        if (NameSceneMap.TryGetValue(sceneName, out ISceneController scene))
        {
            CurrentScene = scene;
            scene.Init();
        }
    }

    public void PlayLevel(GridPointer grid, LevelContext level)
    {
        if (NameSceneMap.TryGetValue(SceneNames.PuzzleLevel, out ISceneController scene))
        {
            CurrentScene = scene;
            var sceneAsPuzzleLevel = CurrentScene as PuzzleLevelSceneController;
            sceneAsPuzzleLevel.Init(grid, level);
        }
    }

    public void ExitGame()
    {
        ShouldExit = true;
    }

    private void UpdateInputs()
    {
        buttonInput.Update();
        mouseInput.Update();
    }

    private void AddScenesToMap()
    {
        var mainMenuScene = new MainMenuSceneController(this, mouseInput, audioManager, textureAtlas, uIFontAtlas);
        NameSceneMap.TryAdd(SceneNames.MainMenu, mainMenuScene);

        var levelEditorScene = new LevelEditorSceneController(this, buttonInput, mouseInput, textureAtlas, uIFontAtlas, shaderManager);
        NameSceneMap.TryAdd(SceneNames.LevelEditor, levelEditorScene);

        var puzzleLevelScene = new PuzzleLevelSceneController(this, buttonInput, audioManager, shaderManager, textureAtlas, uIFontAtlas, window);
        NameSceneMap.TryAdd(SceneNames.PuzzleLevel, puzzleLevelScene);

        var gameCreditsScene = new GameCreditsSceneController(this, mouseInput, audioManager, textureAtlas, uIFontAtlas, window);
        NameSceneMap.TryAdd(SceneNames.GameCredits, gameCreditsScene);
    }
}
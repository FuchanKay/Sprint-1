using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.UIRendering;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas, UIFontAtlas uIFontAtlas) : ISceneController
{
    private readonly static int PlayGameButtonX = 500, PlayGameButtonY = 200;
    private readonly static int ExitGameButtonX = 500, ExitGameButtonY = 300;
    private Button PlayGameButton;
    private Button ExitGameButton;
    private UIFont GameTitle = new (uIFontAtlas.GetFont(FontNames.DungeonFont));
    


    public void Init()
    {
        Console.WriteLine("init");
 
        PlayGameButton = new PlayGameButton(sceneManager, audioManager, textureAtlas);
        PlayGameButton.Init(PlayGameButtonX, PlayGameButtonY, mouseInput);

        ExitGameButton = new ExitGameButton(sceneManager, textureAtlas);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, mouseInput);
        GameTitle.Init( "Sugar Snap", Color.Gold, new Vector2(5,5));

    }

    public void Update(int dtMs)
    {
        PlayGameButton.Update(dtMs);
        ExitGameButton.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        PlayGameButton.Draw(sb);
        ExitGameButton.Draw(sb);
        GameTitle.DrawAtOrigin(sb, new Vector2 (1280/2,40));
    }
}

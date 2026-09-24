using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(IInputManager buttonInput, IInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly IInputManager ButtonInput = buttonInput;
    private readonly IInputManager MouseInput = mouseInput;
    public readonly static string Name = "GamePlay";
    private Object _destructibleObject;
    private Object _pushableObject;
    private HashSet<Object> objects;

    public void Init(ISceneManager sm)
    {
        SceneManager = sm as SceneManager;

        objects = new HashSet<Object>();

        _destructibleObject = new StoneBlock(SceneManager);
        _destructibleObject.Init(new Vector2(300, 100));
        objects.Add(_destructibleObject);


        _pushableObject = new Rock(SceneManager);
        _pushableObject.Init(new Vector2(500, 100));
        objects.Add(_pushableObject);


    }

    public void Update(int dtMs)
    {
        //TODO: Implement game logic here
        foreach (Object obj in objects)
        {
            if(!obj.isDestroyed) {
                obj.Update(dtMs);
                CheckInputs(obj);
            }
        }
    }

    private void CheckInputs(Object obj)
    {
        if(ButtonInput.IsPressed("Destroy")) obj.Destroy();
        if(ButtonInput.IsPressed("Move East")) obj.MoveRight();
        if(ButtonInput.IsPressed("Move West")) obj.MoveLeft();
        if(ButtonInput.IsPressed("Move North")) obj.MoveUp();
        if(ButtonInput.IsPressed("Move South")) obj.MoveDown();
    }

    public void Draw(SpriteBatch sb)
    {
        foreach (Object obj in objects){
            if(!obj.isDestroyed) 
            {
                obj.Draw(sb);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class StoneBlock(SceneManager sm) : Object
{
    private readonly SceneManager SceneManager = sm;
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;
    protected override bool pushable => false;
    protected override bool destructible => true;

    public override void SnapBehavior()
    {
    }
}
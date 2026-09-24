using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Rock(SceneManager sm) : Object
{
    private readonly SceneManager SceneManager = sm;
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;
    protected override bool pushable => true;
    protected override bool destructible => true;
    protected override float scale => 2.5f;

    public override void SnapBehavior()
    {
    }
}
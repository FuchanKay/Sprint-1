// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Scripts.GameComponents;
// namespace Scripts.Game;

// public class Pylon(IAudioManager am, ITextureAtlas textureAtlas) : Object(am, textureAtlas)
// {
//     public static Texture2D ObjectTexture { get; set; }
//     protected override Texture2D Texture => ObjectTexture;
//     protected override Rectangle SourceRectangle => new Rectangle(0, 0, Texture.Width, Texture.Height);
//     protected override bool Pushable => false;
//     protected override bool Destructible => false;
//     protected override float Scale => 2.0f;
//     protected override Vector2 Origin => new Vector2(Texture.Width / 2, Texture.Height / 2);
//     public int SnapPower = 1;

//     public override void SnapBehavior()
//     {
//         if (SnapPower > 0) SnapPower--;
//     }
// }
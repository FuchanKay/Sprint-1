using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class EffectSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float EffectLayerDepth = 0.5f;
    public ISprite CreateEffectSprite(EffectIds effectId, Vector2 coord, LevelContext context)
    {
        ISprite sprite = new AnimatedSprite(textureAtlas);
        var spriteScale = context.GridScale;
        switch (effectId)
        {
            case EffectIds.Explosion:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 8.0f;
                break;
            case EffectIds.PlayerDeath:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.SpringGreen;
                break;
            case EffectIds.RedLaserBeam:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LaserBeam);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.RedLizard);
                sprite.IdleName = RegionNames.LaserBeam;
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.RedLaserImpact:
                sprite.SetState(AnimationNames.LaserImpact);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 4.5f;
                sprite.Color = Color.Orange;
                break;
            case EffectIds.BlueLaserBeam:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LaserBeam);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BlueLizard);
                sprite.IdleName = RegionNames.LaserBeam;
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.BlueLaserImpact:
                sprite.SetState(AnimationNames.LaserImpact);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 4.5f;
                sprite.Color = Color.DodgerBlue;
                break;
            case EffectIds.EnemyDeath:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.DarkRed;
                break;
            case EffectIds.LavaSplash:
                sprite.SetState(AnimationNames.LavaSplash);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.EffectSprites);
                spriteScale = context.GridScale * 5.0f;
                break;
            case EffectIds.WaterSplash:
                sprite.SetState(AnimationNames.WaterSplash);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.EffectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.WarlockSpell:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.Purple;
                break;
            case EffectIds.Slash:
                sprite.SetState(AnimationNames.Slash);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Slash);
                sprite.IdleName = RegionNames.Slash;
                spriteScale = context.GridScale * 0.8f;
                sprite.Color = Color.White;
                break;
            default:
                break;
        }
        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);
        sprite.LayerDepth = EffectLayerDepth;
        sprite.Scale = spriteScale;
        return sprite;
    }
}
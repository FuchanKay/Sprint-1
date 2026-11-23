using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public class ShaderManager()
{
    private readonly Dictionary<string, Effect> EffectMap = [];
    public void MapEffect(string effectName, Effect effect)
    {
        if(!EffectMap.TryAdd(effectName, effect))
        {
            EffectMap[effectName] = effect;
        }
    }

    public Effect GetEffect(string effectName)
    {
        if(EffectMap.ContainsKey(effectName)) return EffectMap[effectName];
        return null;
    }
}
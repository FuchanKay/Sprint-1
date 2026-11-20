#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

float2 PlayerPosition;
float2 ScreenSize;
float Radius;
float FadeWidth;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	// Sample the texture
    float2 pixelPosition = input.TextureCoordinates * ScreenSize;
	float distanceToPlayer = distance(pixelPosition, PlayerPosition);
	float alpha = saturate((distanceToPlayer - Radius) / FadeWidth);

    return float4(0, 0, 0, alpha);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D ExploredTexture;

float2 PlayerPosition;
float2 ScreenSize;
float Radius;
float FadeWidth;

// Samples each pixel at a position (U, V)
sampler2D ExploredSampler = sampler_state
{
    Texture = <ExploredTexture>;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 pixelPosition = input.TextureCoordinates * ScreenSize;

	float currentReveal = 1.0 - saturate((distance(pixelPosition, PlayerPosition) - Radius) / FadeWidth);
	float explored = tex2D(ExploredSampler, input.TextureCoordinates).r;

	float fogAlpha = 1.0 - max(explored, currentReveal);
	return float4(0, 0, 0, fogAlpha);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
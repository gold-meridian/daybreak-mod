sampler uImage0 : register(s0);

float4 uPanelDimensions;
float2 uScreenSize;

#define FADE_DISTANCE_TOP 8.0
#define FADE_DISTANCE_BOTTOM 32.0
#define POSITION uPanelDimensions.xy
#define SIZE uPanelDimensions.zw

float4 main(float2 coords : SV_POSITION, float2 tex_coords : TEXCOORD0) : COLOR0
{
    float2 local_coords = coords - POSITION;
    // float2 uv = local_coords / SIZE;
    
    float4 c = tex2D(uImage0, tex_coords);
    
    float top_fade = saturate(local_coords.y / FADE_DISTANCE_TOP);
    float bottom_fade = saturate((SIZE.y - local_coords.y) / FADE_DISTANCE_BOTTOM);
    float fade = min(top_fade, bottom_fade);
    
    c *= pow(fade, 2);
    
    return c;
}

#ifdef FX
technique Technique1
{
    pass FadeShader
    {
        PixelShader = compile ps_3_0 main();
    }
}
#endif // FX

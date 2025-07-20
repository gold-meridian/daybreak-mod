#include "../pixelation.h"

sampler uImage0 : register(s0);

texture uTexture;
sampler tex0 = sampler_state
{
    texture = <uTexture>;
    magfilter = POINT;
    minfilter = POINT;
    mipfilter = POINT;
    AddressU = wrap;
    AddressV = wrap;
};

float uTime;
float4 uSource;
float uHoverIntensity;
float uPixel;
float uColorResolution;
float uGrayness;
float4 uColor;
float4 uSecondaryColor;
float uSpeed;

float4 main(float2 coords : SV_POSITION, float2 tex_coords : TEXCOORD0, float4 baseColor : COLOR0) : COLOR0
{
    float2 resolution = uSource.xy;
    float2 position = uSource.zw;
    coords -= position;
    float2 uv = normalize_with_pixelation(coords, uPixel, float2(resolution.x, resolution.y));
    
    float redWobble = sin(uTime * uSpeed * 3.1415);
    float4 redFade = lerp(uColor * (1 + uHoverIntensity * 0.2), uSecondaryColor, uv.x * 2 - redWobble * 0.1);
    float4 panelColor = lerp(redFade, baseColor - 0.05 + uHoverIntensity * 0.1, pow(uv.x, 1.2 + redWobble * 0.3));
    panelColor.rgb *= 1.2 + sin(uv.x * 11 + uv.y * 2 - uTime * uSpeed * 3.1415) * 0.3 * (1 - uv.x * 0.1);
    return tex2D(uImage0, tex_coords) * panelColor;
}

#ifdef FX
technique Technique1
{
    pass PanelShader
    {
        PixelShader = compile ps_3_0 main();
    }
}
#endif // FX

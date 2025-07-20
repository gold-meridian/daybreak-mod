#include "../pixelation.h"

sampler uImage0 : register(s0);

float uTime;
float uHoverIntensity;
float uPixel;
float uColorResolution;
float uGrayness;
float uSpeed;
float4 uSource;
float3 uInColor;

float3 rayGun(float2 ray, float2 uv, float3 col, float delay, float size, float len)
{
    float3 sunCol = float3(1.0, 0.9, 0.8);
    float ang = dot(normalize(uv), normalize(ray));
    ang = 1.0 - ang;
    ang = ang / size;
    ang = clamp(ang, 0.0, 1.0);

    float v = smoothstep(0.0, 1.0, (1.0 - ang) * (1.0 - ang));
    v *= sin(uTime / 2.0 + delay) / 2.0 + 0.5;

    float l = length(uv) * len;
    l = clamp(l, 0.0, 1.0);

    float3 o = lerp(col, lerp(sunCol, col, l), smoothstep(1.0, 4.0, uTime) * v);
    return o;
}

float hash11(float p)
{
    p = frac(p * .1031);
    p *= p + 34.32;
    p *= p + p;
    return frac(p);
}

#define PI 3.14159265359

float4 main(float2 uv : TEXCOORD0, float4 baseColor : COLOR0) : COLOR0
{
    uv = normalize_with_pixelation(uv * uSource.zw, uPixel, uSource.zw);

    float t = uTime / 15;
    float2x2 rotTime = float2x2(cos(t), sin(t), -sin(t), cos(t));
    float2 ray = mul(float2(1.0, 0.0), rotTime);
    float3 col = float3(0, 0, 0);
    float3 sunCol = float3(1.0, 0.9, 0.8);

    for (float x = 0.0; x < 2.0 * PI; x += PI / 8.0)
    {
        float2x2 rot = float2x2(cos(x), sin(x), -sin(x), cos(x));
        float2 ray = mul(mul(rotTime, rot), float2(1.0, 0.0));
        col = rayGun(ray, uv - 0.5, col, hash11(x * 15.0) * 7003.0, hash11(x * 234.0) * 0.2, 2);
    }
    
    float3 colorLerp = lerp(uInColor, baseColor.rgb, length(pow(col * 1.5, 3)));

    return float4(colorLerp, baseColor.a) * smoothstep(0.5, 1.0, length(col * 3)) * length(pow(col * 1.5, 2));
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

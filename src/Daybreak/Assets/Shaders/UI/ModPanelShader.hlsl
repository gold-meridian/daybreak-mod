#include "../tmlbuild.h"

sampler uImage0 : register(s0);

float uTime GLOBAL_TIME;
float4 uSource;
float uHoverIntensity;
float uPixel;
float uColorResolution;
float uGrayness;
float3 uInColor;
float uSpeed;

float rand(float2 uv)
{
    float x = dot(uv, float2(4371.321, -9137.327));
    return 2.0 * frac(sin(x) * 17381.94472) - 1.0;
}

float noise(in float2 uv)
{
    float2 id = floor(uv);
    float2 f = frac(uv);
    float2 u = f * f * (3.0 - 2.0 * f);

    return lerp(lerp(rand(id + float2(0.0, 0.0)),
                     rand(id + float2(1.0, 0.0)), u.x),
                lerp(rand(id + float2(0.0, 1.0)),
                     rand(id + float2(1.0, 1.0)), u.x),
                u.y);
}

float fbm(float2 uv)
{
    float f = 0.0;
    float gat = 0.0;

    for (float octave = 0.; octave < 5.; ++octave)
    {
        float la = pow(2.0, octave);
        float ga = pow(0.5, octave + 1.);
        f += ga * noise(la * uv);
        gat += ga;
    }

    f = f / gat;

    return f;
}

float3 hash33(float3 p3)
{
    float3 uv = frac(p3 * float3(.1031, .11369, .13787));
    uv += dot(uv, uv.yxz + 19.19);
    return -1.0 + 2.0 * frac(float3((uv.x + uv.y) * uv.z, (uv.x + uv.z) * uv.y, (uv.y + uv.z) * uv.x));
}

float3 glsl_mod(float3 x, float3 y)
{
    return x - y * floor(x / y);
}

float worley(float3 uv, float scale)
{
    float3 id = floor(uv);
    float3 fd = frac(uv);

    float n = 0.;

    float minimalDist = 1.;


    for (float x = -2.; x <= 2.; x++)
    {
        for (float y = -2.; y <= 2.; y++)
        {
            for (float z = -2.; z <= 2.; z++)
            {
                float3 coord = float3(x, y, z);
                float3 rId = hash33(glsl_mod(abs(id + coord), scale)) * 0.33;

                float3 r = coord + rId - fd;

                float d = dot(r, r);

                if (d < minimalDist)
                {
                    minimalDist = d;
                }
            } //z
        } //y
    } //x

    return minimalDist;
}

float fbm(float3 uv, float scale)
{
    float G = exp(-0.3);
    float amp = 1.;
    float freq = 1.;
    float n = 0.;

    for (int i = 0; i < 5; i++)
    {
        n += worley(uv * freq, scale * freq) * amp;
        freq *= 2.;
        amp *= G;
    }

    return n * n;
}

float fbm_warped(float2 uv)
{
    float h = fbm(0.09 * uTime + uv + fbm(0.065 * uTime + 2.0 * uv - 5.0 * fbm(4.0 * uv)));
    return fbm(h.xxx, 1.0);
}

float border(float2 uv, float epsilion)
{
    float f = fbm_warped(uv - float2(0.0, 0.0));

    float left = fbm_warped(uv - float2(0.0, epsilion));
    float up = fbm_warped(uv - float2(epsilion, 0.0));
    float right = fbm_warped(uv + float2(0.0, epsilion));
    float down = fbm_warped(uv + float2(epsilion, 0.0));

    float border_o = clamp(abs(4. * f - left - down - up - right), 0., 1.);

    return border_o;
}

float mySmoothstep(float edge0, float edge1, float x)
{
    x = clamp((x - edge0) / (edge1 - edge0), 0.0, 1.0);
    return x * x * (3. - 2. * x);
}

float4 main(float2 coords : SV_POSITION, float2 tex_coords : TEXCOORD0) : COLOR0
{
    float2 resolution = 800.0f.xx;
    float2 position = uSource.zw;

    coords -= position;

    // Normalize the coordinates but with optional pixelation.
    float2 uv = coords / resolution;
    float f = fbm(float3(uv, 2.0), 2.0);

    float a2 = smoothstep(-0.5, 0.5, f);
    float a1 = smoothstep(-1.0, 1.0, fbm(uv));

    float3 finalCol = lerp(lerp(float3(254. / 255., 18. / 255., 97. / 255.),
                                float3(253. / 255., 248. / 255., 27. / 255.), a1),
                           float3(255. / 255., 122. / 255., 2. / 255.), a2);


    float3 outline = float3(177. / 255., 100. / 255., 100. / 255.) * border(uv, 0.00025);
    finalCol += outline;

    finalCol = lerp(
        outline,
        finalCol,
        mySmoothstep(0., 1., uHoverIntensity)
    );

    return float4(finalCol, 1.);
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

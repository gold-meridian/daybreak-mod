#ifndef PIXELATION_HLSL
#define PIXELATION_HLSL

/**
 * Applies pixelation to the given coordinates based on a defined pixel size and
 * outputs the normalized coordinates.
 * 
 * @param coords The coordinates within the resolution space.
 * @param pixel_size The size of each pixel block.
 * @param resolution The resolution with which to normalize the coords by.
 * @return The normalized, pixelated texture coordinates.
 */
float2 normalize_with_pixelation(float2 coords, float pixel_size, float2 resolution)
{
    return floor(coords / pixel_size) / (resolution / pixel_size);
}

float2 pixelate(float2 coords, float pixel_size)
{
    return floor(coords / pixel_size) * pixel_size;
}

/**
 * Quantizes the input color to a given resolution.
 *
 * @param color The input color.
 * @param color_resolution The resolution of quantization.
 * @return The quantized color.
 */
float3 quantize_color(float3 color, float3 color_resolution)
{
    return floor(color * color_resolution) / (color_resolution - 1.0f);
}

float4 quantize_color_with_alpha(float4 color, float4 color_resolution)
{
    return floor(color * color_resolution) / (color_resolution - 1.0f);
}

#endif // PIXELATION_HLSL
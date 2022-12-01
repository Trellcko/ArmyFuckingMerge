#ifndef BACKFACEOUTLINES_INCLUDED // to prevent this hlsl be including twice
#define BACKFACEOUTLINES_INCLUDED

// Urp cool help Funcitions
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Input
{
    float4 positionOS: POSITION; // position in object space
    float3 normalOS : NORMAL; // normal vector in object space
#ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    float3 smoothNormalsOS : TEXCOORD1;
#endif
};

struct Output
{
    float4 positionCS : SV_POSITION; //Position in clip space
};

float _Thickness;
float4 _Color;

Output Vertex(Input input)
{
    Output output = (Output)0;
#ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    float3 normalOS = input.smoothNormalsOS;
#else
    float3 normalOS = input.normalOS;
#endif
    float3 positionOS = input.positionOS.xyz + normalOS * _Thickness;
    output.positionCS = GetVertexPositionInputs(positionOS).positionCS;
    
    return output;
}

float4 Fragment(Output output) : SV_TARGET
{
    return _Color;
}

#endif
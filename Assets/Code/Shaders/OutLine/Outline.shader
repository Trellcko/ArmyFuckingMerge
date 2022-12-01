Shader "Unlit/Outline"
{
    Properties
    {
        _Thickness("Thickness", Float) = 1
        _Color("Color", Color) = (1, 1, 1, 1)
        [Toggle(USE_PRECALCULATED_OUTLINE_NORMALS)]_PrecalculatedNormals("Use UV1 normals", Float) = 0
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }
        
        Pass{
            // draw only the front faces of the object so that the outline model is not drawn on top of the object
            
            Name "Outlines"

            Cull Front

            HLSLPROGRAM
            // URP requirements
            #pragma prefer_hlslcc gless
            #pragma exclude_renderers d3d11_9x
            #pragma shader_feature USE_PRECALCULATED_OUTLINE_NORMALS

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Outline.hlsl"

            ENDHLSL
            }
    }
}

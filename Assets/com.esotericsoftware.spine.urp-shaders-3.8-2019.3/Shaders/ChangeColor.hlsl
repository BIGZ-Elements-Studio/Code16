CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
// Other includes...

sampler2D _MainTex;
float4 _FillColor;
float _FillPhase;
float4 _ChangeColor;

// Other declarations...

struct VertexInput {
    // Input structure...
};

struct VertexOutput {
    // Output structure...
};

VertexOutput vert (VertexInput v) {
    // Vertex function...
}

float4 frag (VertexOutput i) : SV_Target {
    float4 rawColor = tex2D(_MainTex, i.uv);
    float finalAlpha = (rawColor.a * i.vertexColor.a);

    #if defined(_STRAIGHT_ALPHA_INPUT)
    rawColor.rgb *= rawColor.a;
    #endif

    float3 finalColor = lerp((rawColor.rgb * i.vertexColor.rgb), (_FillColor.rgb * finalAlpha), _FillPhase); // make sure to PMA _FillColor.
    
    // Apply color change
    finalColor *= _ChangeColor.rgb;

    return fixed4(finalColor, finalAlpha);
}
ENDCG
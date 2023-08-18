Shader "Custom/Exit1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Emission ("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionStrength ("Emission Strength", Range(0, 1)) = 1
        _Color ("Color", Color) = (1, 1, 1, 1)
        _VideoTex ("Video Texture", 2D) = "white" {} // Add the video texture property
        _Transparency ("Transparency", Range(0, 1)) = 1 // Add transparency property
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _Emission;
            float _EmissionStrength;
            float4 _Color;
            sampler2D _VideoTex; // Declare the video texture sampler
            float _Transparency;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb += _Emission.rgb * _EmissionStrength;
                col *= _Color;
                
                // Sample the video texture
                fixed4 videoCol = tex2D(_VideoTex, i.uv);
                col = lerp(col, videoCol * _Transparency, videoCol.a); // Mix with the video texture using transparency

                return col;
            }
            ENDCG
        }
    }
}
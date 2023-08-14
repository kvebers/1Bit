Shader "Custom/EmissionLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Emission ("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionStrength ("Emission Strength", Range(0, 1)) = 1
        _Color ("Color", Color) = (1, 1, 1, 1) // Add _Color property
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
            float4 _Color; // Declare _Color variable

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
                col *= _Color; // Apply _Color property
                return col;
            }
            ENDCG
        }
    }
}

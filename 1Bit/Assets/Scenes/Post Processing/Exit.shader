Shader "Custom/Exit" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Emission ("Emission", Color) = (1, 1, 1, 1)
        _Speed ("Speed", Range(0, 10)) = 1
    }

    SubShader {
        Tags { "Queue" = "Transparent" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {  
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Speed;
            fixed4 _Emission;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Calculate scrolling offset based on time
                float2 offset = float2(i.uv.x + _Time.y * _Speed, i.uv.y);

                // Sample the texture and apply emissive effect
                fixed4 col = tex2D(_MainTex, offset) * _Emission;

                return col;
            }
            ENDCG
        }
    }
}
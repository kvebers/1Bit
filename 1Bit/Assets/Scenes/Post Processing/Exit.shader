Shader "Custom/Exit" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            float grayscale = dot(c.rgb, float3(0.3, 0.59, 0.11));
            o.Albedo = grayscale;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

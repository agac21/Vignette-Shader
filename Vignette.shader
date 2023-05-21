Shader "Custom/Vignette"
{
    Properties
    {
        [HideInInspector]_Color ("Tint Color", Color) = (0, 0, 0, 1)
        [HideInInspector]_Intensity ("Vignette Intensity", Range(0, 1)) = 0.5
        [HideInInspector]_Radius ("Radius", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Intensity;
            float _Radius;
            float4 _Color;
            sampler2D _CameraColorTexture;


            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv * 2.0 - 1.0;
                float4 col = tex2D(_CameraColorTexture, i.uv);
                float circle = length(uv);
                float mask = 1 - smoothstep(_Radius, _Radius + _Intensity, circle);
                float invertMask = 1 - mask;
                float3 displayColor = col.rgb * mask;
                float3 vignetteColor = (1 - col.rgb) * _Color * invertMask;
                return half4(displayColor + vignetteColor, 1);
            }
            ENDCG
        }
    }
}
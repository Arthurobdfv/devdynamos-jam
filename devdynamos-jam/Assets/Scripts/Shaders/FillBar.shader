Shader "Unlit/FillBar"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _FillBar("FillBar", Range(0.0, 1.0)) = 0
        _AlphaRate("AlphaRate", Range(0.0, 1.0)) = 1
        _GradientInitialColor("GradientInitialColor", Color) = (1,1,1,1)
        _GradientFinalColor("GradientFinalColor", Color) = (1,1,1,1)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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

            sampler2D _MainTex;
            float _FillBar;
            float _AlphaRate;
            float4 _GradientInitialColor;
            float4 _GradientFinalColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float fill = _FillBar > i.uv.y;
                float4 color = lerp(_GradientInitialColor, _GradientFinalColor, _FillBar);
                clip(fill - 1);
                color.a = _AlphaRate;
                return color;
        }
        ENDCG
    }
    }
}

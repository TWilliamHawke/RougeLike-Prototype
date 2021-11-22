Shader "Hidden/FogOfWarShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SecTex ("Secondary Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+1"
        }
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _SecTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_SecTex, i.uv);
                col.a = saturate(2 - col.r * 1.5 - col.b * 0.5);

                return fixed4(0,0,0,col.a);
            }
            ENDCG
        }
    }
}

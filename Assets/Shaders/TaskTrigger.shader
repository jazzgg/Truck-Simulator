Shader "Custom/TaskTrigger"
{
    Properties{
	_Color("Main Color", Color) = (1,1,1,1)
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Emission ("Emission", Range(0, 1)) = 0
	}

	SubShader{
	Tags{ "Queue" = "Transparent"  "RenderType" = "Transparent" }
	Cull off
	LOD 200

	CGPROGRAM
	#pragma surface surf Lambert alpha:fade

	sampler2D _MainTex;
	fixed4 _Color;
	float _Emission;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Emission = _Emission;
		o.Alpha = 1 - IN.uv_MainTex.y; // just invert uv 
	}
	ENDCG
	}
    
    Fallback "Legacy Shaders/Transparent/VertexLit"
}

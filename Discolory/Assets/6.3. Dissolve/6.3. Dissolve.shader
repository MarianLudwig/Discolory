Shader "ShaderCourse/Week6/6.3. Dissolve" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color ("Colour", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		[HDR] _EmissionColor ("Color", Color) = (0,0,0,1)
		_EmissionMap("Emission", 2D) = "white"{}

		[HDR] _DissolveGlowColor ("Dissolve glow colour", Color) = (1,1,1,1)
		_Dissolve ("Dissolve", Range(0,1)) = 0
		_DissolveGlowOffset ("Dissolve glow offset", Range(0,1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque"}
		Cull Off

		CGPROGRAM

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow
		#pragma target 5.0
		#include "noiseSimplex.cginc"
		
		sampler2D _MainTex;
		sampler2D _EmissionMap;

		struct Input {
			float3 worldPos;
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _EmissionColor;

		half _Dissolve;
		half _DissolveGlowOffset;
		fixed4 _DissolveGlowColor;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			float noise = (snoise(float3(IN.worldPos * 1.0f)) + 1) / 2;
			clip(noise - _Dissolve);
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color.rgb;
			o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor.rgb;
			if(noise - _Dissolve < _DissolveGlowOffset && noise > _DissolveGlowOffset)
			{
				o.Emission = _DissolveGlowColor.rgb;
				o.Albedo = _DissolveGlowColor.rgb;
			}
		}

		ENDCG
	}
	FallBack "Diffuse"
}

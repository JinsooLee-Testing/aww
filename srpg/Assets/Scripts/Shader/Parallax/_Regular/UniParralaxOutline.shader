﻿// Shader created by N1warhead
// www.warhead-designz.com


Shader "UniToonUltra/Parrallax/NormalOutline" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_OutlineColor ("Outline Color", Color) = (0,0,0,1)
	_Outline ("Outline width", Range (.002, 0.03)) = .005
	_Ramp ("Shading Ramp", 2D) = "gray" {}
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_Parallax ("Height", Range (0.005, 0.08)) = 0.02
	_ParallaxMap ("Heightmap (A)", 2D) = "black" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 500

CGPROGRAM
#pragma surface surf Ramp

sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _ParallaxMap;
fixed4 _Color;
float _Parallax;
sampler2D _Ramp;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float3 viewDir;
};

half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * 0.5 + 0.5;
			half3 ramp = tex2D (_Ramp, float2(diff, diff)).rgb;
			half4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff * ramp) * (atten * 2);
			c.a = s.Alpha;
			return c;
		}

void surf (Input IN, inout SurfaceOutput o) {
	half h = tex2D (_ParallaxMap, IN.uv_BumpMap).w;
	float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
	IN.uv_MainTex += offset;
	IN.uv_BumpMap += offset;
	
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color * 2;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
ENDCG 
CGINCLUDE
		#include "UnityCG.cginc"
 
		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};
 
		struct v2f {
			float4 pos : POSITION;
			float4 color : COLOR;
		};
 
		uniform float _Outline;
		uniform float4 _OutlineColor;
 
		v2f vert(appdata v) {
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
 
			float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
			float2 offset = TransformViewToProjection(norm.xy);
 
			o.pos.xy += offset * o.pos.z * _Outline;
			o.color = _OutlineColor;
			return o;
		}
		ENDCG
 
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}
 
    }
 
	Fallback "Bumped Diffuse"
}
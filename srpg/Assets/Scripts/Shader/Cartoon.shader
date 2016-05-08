Shader "NPR Cartoon Effect/Cartoon" {
	Properties {
		_MainTex ("Base Texture", 2D) = "white" {}
		_RampTex ("Ramp Texture", 2D) = "white" {}
		_DiffuseColor ("Diffuse Color", Color) = (1, 1, 1, 1)
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_SpecularScale ("Specular Scale", Range(0, 0.05)) = 0.01
		_SpecularTranslationX ("Specular Translation X", Range(-1, 1)) = 0
		_SpecularTranslationY ("Specular Translation Y", Range(-1, 1)) = 0
		_SpecularRotationX ("Specular Rotation X", Range(-180, 180)) = 0
		_SpecularRotationY ("Specular Rotation Y", Range(-180, 180)) = 0
		_SpecularRotationZ ("Specular Rotation Z", Range(-180, 180)) = 0
		_SpecularScaleX ("Specular Scale X", Range(-1, 1)) = 0
		_SpecularScaleY ("Specular Scale Y", Range(-1, 1)) = 0
		_SpecularSplitX ("Specular Split X", Range(0, 1)) = 0
		_SpecularSplitY ("Specular Split Y", Range(0, 1)) = 0
		_OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineWidth ("Outline Width", Range(0, 1)) = 0.005
	}
	SubShader {
		Pass {
			Name "OUTLINE"
			Tags { "RenderType" = "Opaque" "LightMode" = "Always" }
			Cull Back
			
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#pragma multi_compile_fwdbase
			#pragma glsl
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _RampTex;
			uniform float4 _DiffuseColor;
			uniform float4 _SpecularColor;
			uniform float _SpecularScale;
			uniform float _SpecularTranslationX;
			uniform float _SpecularTranslationY;
			uniform float _SpecularRotationX;
			uniform float _SpecularRotationY;
			uniform float _SpecularRotationZ;
			uniform float _SpecularScaleX;
			uniform float _SpecularScaleY;
			uniform float _SpecularSplitX;
			uniform float _SpecularSplitY;

			struct VSOutput
			{
				float4 pos : SV_POSITION;
				float2 tex : TEXCOORD0;
				float3 tgsnor : TEXCOORD1;    // tangent space normal
				float3 tgslit : TEXCOORD2;    // tangent space light
				float3 tgsview : TEXCOORD3;   // tangent space view
				LIGHTING_COORDS(4, 5)
			};
			VSOutput vert (appdata_tan v)
			{
				VSOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = TRANSFORM_TEX(v.texcoord, _MainTex);
				TANGENT_SPACE_ROTATION;
				o.tgsnor = mul(rotation, v.normal);
				o.tgslit = mul(rotation, ObjSpaceLightDir(v.vertex));
				o.tgsview = mul(rotation, ObjSpaceViewDir(v.vertex));
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}
			float4 frag (VSOutput i) : COLOR
			{
				float3 N = normalize(i.tgsnor);
				float3 L = normalize(i.tgslit);
				float3 V = normalize(i.tgsview);
				float3 H = normalize(V + L);
				
				// specular highlights scale
				H = H - _SpecularScaleX * H.x * float3(1, 0, 0);
				H = normalize(H);
				H = H - _SpecularScaleY * H.y * float3(0, 1, 0);
				H = normalize(H);
				
				// specular highlights rotation
				#define DegreeToRadian 0.0174533
				float radX = _SpecularRotationX * DegreeToRadian;
				float3x3 rotMatX = float3x3(
					1,	0, 		 	0,
					0,	cos(radX),	sin(radX),
					0,	-sin(radX),	cos(radX));
				float radY = _SpecularRotationY * DegreeToRadian;
				float3x3 rotMatY = float3x3(
					cos(radY), 	0, 		-sin(radY),
					0,			1,		0,
					sin(radY), 	0, 		cos(radY));
				float radZ = _SpecularRotationZ * DegreeToRadian;
				float3x3 rotMatZ = float3x3(
					cos(radZ), 	sin(radZ), 	0,
					-sin(radZ), cos(radZ), 	0,
					0, 			0,			1);
				H = mul(rotMatZ, mul(rotMatY, mul(rotMatX, H)));
				H = normalize(H);
				
				// specular highlights translation
				H = H + float3(_SpecularTranslationX, _SpecularTranslationY, 0);
				H = normalize(H);
				
				// specular highlights split
				float signX = 1;
				if (H.x < 0)
					signX = -1;

				float signY = 1;
				if (H.y < 0)
					signY = -1;

				H = H - _SpecularSplitX * signX * float3(1, 0, 0) - _SpecularSplitY * signY * float3(0, 1, 0);
				H = normalize(H);
				
				//
				// cartoon light model
				//
				
				// ambient light from Unity render setting
				float3 ambientColor = UNITY_LIGHTMODEL_AMBIENT.xyz;

				// diffuse cartoon light
				float diff = dot(N, L);
				diff = (diff * 0.5 + 0.5) * LIGHT_ATTENUATION(i);
				float4 diffuseColor = tex2D(_MainTex, i.tex) * _DiffuseColor * tex2D(_RampTex, float2(diff, diff));
				
				// stylized specular light
				float spec = dot(N, H);
				float w = fwidth(spec);
				float3 specularColor = lerp(float3(0, 0, 0), _SpecularColor.rgb, smoothstep(-w, w, spec + _SpecularScale - 1));
				
				return float4(ambientColor + diffuseColor.rgb + specularColor, 1.0) * _LightColor0;
            }
			ENDCG
		}
		Pass {
			Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
			Cull Front

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			uniform float4 _OutlineColor;
			uniform float _OutlineWidth;
			struct VSOutput
			{
				float4 pos : SV_POSITION;
			};
			VSOutput vert (appdata_base v)
			{
				VSOutput o;
				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				norm = normalize(norm);
				float2 offset = TransformViewToProjection(norm.xy);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.pos.xy += offset * o.pos.z * _OutlineWidth;
				return o;
			}
			float4 frag (VSOutput i) : COLOR
			{
				return _OutlineColor;
            }
			ENDCG
		}
	} 
	FallBack "Diffuse"
}

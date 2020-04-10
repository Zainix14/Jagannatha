// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BlendGlowCenter"
{
	Properties
	{
		_SpeedMainTextUVNoise("Speed MainText U/V + Noise", Vector) = (0,0,0,0)
		_MainTex("Main Tex", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Color0("Color 0", Color) = (0.3773585,0.3755785,0.3755785,1)
		[Toggle(_USECENTERGLOW_ON)] _UseCenterGlow("Use Center Glow?", Float) = 0
		_DepthPower("DepthPower", Float) = 1
		[Toggle(_USEDEPTH_ON)] _UseDepth("Use Depth?", Float) = 0
		_Opacity("Opacity", Range( 0 , 1)) = 1
		_Emission("Emission", Float) = 2
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
	LOD 100

		Cull Off
		CGINCLUDE
		#pragma target 3.0 
		ENDCG
		
		
		Pass
		{
			
			Name "ForwardBase"
			Tags { "LightMode"="ForwardBase" }

			CGINCLUDE
			#pragma target 3.0
			ENDCG
			Blend Off
			Cull Back
			ColorMask RGBA
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define UNITY_PASS_FORWARDBASE
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma shader_feature _USECENTERGLOW_ON
			#pragma shader_feature _USEDEPTH_ON

			uniform sampler2D _MainTex;
			uniform float4 _SpeedMainTextUVNoise;
			uniform sampler2D _Noise;
			uniform float4 _Color0;
			uniform sampler2D _Mask;
			uniform float4 _Mask_ST;
			uniform float _Emission;
			uniform sampler2D _TextureSample0;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform float _DepthPower;
			uniform float _Opacity;


			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
			};
			
			struct v2f
			{
				float4 pos : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				float4 ase_texcoord2 : TEXCOORD2;
			};
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord2 = screenPos;
				
				o.ase_texcoord1 = v.ase_texcoord;
				o.ase_color = v.ase_color;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.pos = UnityObjectToClipPos(v.vertex);
				#if ASE_SHADOWS
					#if UNITY_VERSION >= 560
						UNITY_TRANSFER_SHADOW( o, v.texcoord );
					#else
						TRANSFER_SHADOW( o );
					#endif
				#endif
				return o;
			}
			
			float4 frag (v2f i ) : SV_Target
			{
				float3 outColor;
				float outAlpha;

				float2 appendResult4 = (float2(_SpeedMainTextUVNoise.x , _SpeedMainTextUVNoise.y));
				float4 uv08 = i.ase_texcoord1;
				uv08.xy = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_13_0 = (uv08).xy;
				float2 temp_output_11_0 = ( ( appendResult4 * _Time.y ) + temp_output_13_0 );
				float2 appendResult5 = (float2(_SpeedMainTextUVNoise.z , _SpeedMainTextUVNoise.w));
				float4 tex2DNode17 = tex2D( _Noise, ( temp_output_13_0 + ( _Time.y * appendResult5 ) ) );
				float3 temp_output_29_0 = ( (tex2D( _MainTex, temp_output_11_0 )).rgb * (tex2DNode17).rgb * (_Color0).rgb * (i.ase_color).rgb );
				float2 uv_Mask = i.ase_texcoord1.xy * _Mask_ST.xy + _Mask_ST.zw;
				float3 temp_output_30_0 = (tex2D( _Mask, uv_Mask )).rgb;
				float3 temp_cast_0 = ((-1.0 + (uv08.z - 0.0) * (1.0 - -1.0) / (1.0 - 0.0))).xxx;
				float3 clampResult32 = clamp( ( temp_output_30_0 - temp_cast_0 ) , float3( 0,0,0 ) , float3( 1,0,0 ) );
				float3 clampResult34 = clamp( ( temp_output_30_0 * clampResult32 ) , float3( 0,0,0 ) , float3( 1,0,0 ) );
				#ifdef _USECENTERGLOW_ON
				float3 staticSwitch36 = ( temp_output_29_0 * clampResult34 );
				#else
				float3 staticSwitch36 = temp_output_29_0;
				#endif
				
				float temp_output_37_0 = ( tex2D( _TextureSample0, temp_output_11_0 ).a * tex2DNode17.a * _Color0.a * i.ase_color.a );
				float4 screenPos = i.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth39 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
				float distanceDepth39 = abs( ( screenDepth39 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthPower ) );
				float clampResult40 = clamp( distanceDepth39 , 0.0 , 1.0 );
				#ifdef _USEDEPTH_ON
				float staticSwitch42 = ( temp_output_37_0 * clampResult40 );
				#else
				float staticSwitch42 = temp_output_37_0;
				#endif
				
				
				outColor = ( staticSwitch36 * _Emission );
				outAlpha = ( staticSwitch42 * _Opacity );
				clip(outAlpha);
				return float4(outColor,outAlpha);
			}
			ENDCG
		}
		
	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17700
1229;171;1154;795;1776.106;595.5977;3.184456;True;False
Node;AmplifyShaderEditor.Vector4Node;1;-2317.237,-23.29916;Inherit;False;Property;_SpeedMainTextUVNoise;Speed MainText U/V + Noise;1;0;Create;True;0;0;False;0;0,0,0,0;0,3,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1596.113,-10.79419;Inherit;True;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;3;-1857.194,27.61603;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;4;-2002.194,-153.384;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;18;-1674.684,619.4446;Inherit;True;Property;_Mask;Mask;4;0;Create;True;0;0;False;0;-1;None;b3bf351bbc112f74088194082abfdf8b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;5;-2011.194,199.616;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;19;-1298.635,956.0001;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1622.194,192.616;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;30;-1348.43,694.9977;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;13;-1301.1,17.19191;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1669.194,-125.384;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1127.179,-187.1116;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;31;-1063.915,870.1439;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1116.648,232.5798;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-138.386,909.6378;Inherit;False;Property;_DepthPower;DepthPower;7;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-896.8268,353.291;Inherit;False;Property;_Color0;Color 0;5;0;Create;True;0;0;False;0;0.3773585,0.3755785,0.3755785,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;32;-876.7486,880.4468;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;16;-927.6148,-188.6291;Inherit;True;Property;_MainTex;Main Tex;2;0;Create;True;0;0;False;0;-1;None;73a051e8f39182845820c0c3f23514a3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;-921.4507,127.7909;Inherit;True;Property;_Noise;Noise;3;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;21;-871.0704,557.6283;Inherit;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;39;60.79972,842.6703;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;27;-632.1128,361.8767;Inherit;True;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;26;-606.6335,183.2962;Inherit;True;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;24;-607.3466,-56.76726;Inherit;True;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-636.3517,751.6627;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;28;-541.9454,549.8001;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;59;-797.157,-453.1132;Inherit;True;Property;_TextureSample0;Texture Sample 0;11;0;Create;True;0;0;False;0;-1;None;b3bf351bbc112f74088194082abfdf8b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;34;-464.64,760.2486;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-258.0583,190.1647;Inherit;True;4;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;40;313.2164,746.5113;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;105.4445,564.4971;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;488.3628,686.4123;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;5.884982,271.3457;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;46;485.3777,298.9909;Inherit;False;Property;_Emission;Emission;10;0;Create;True;0;0;False;0;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;36;206.7321,209.053;Inherit;True;Property;_UseCenterGlow;Use Center Glow?;6;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;43;560.0925,848.6709;Inherit;True;Property;_Opacity;Opacity;9;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;42;627.0327,609.1419;Inherit;False;Property;_UseDepth;Use Depth?;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;661.7919,234.8098;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;881.3474,576.9383;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;69;1210.5,376.8124;Float;False;False;-1;2;ASEMaterialInspector;100;8;New Amplify Shader;e1de45c0d41f68c41b2cc20c8b9c05ef;True;ForwardAdd;0;1;ForwardAdd;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;True;2;0;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;True;2;False;-1;False;False;True;1;LightMode=ForwardAdd;False;0;;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;70;1210.5,440.8124;Float;False;False;-1;2;ASEMaterialInspector;100;8;New Amplify Shader;e1de45c0d41f68c41b2cc20c8b9c05ef;True;Deferred;0;2;Deferred;4;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;True;2;0;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=Deferred;True;2;0;;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;68;1210.5,269.8124;Float;False;True;-1;2;ASEMaterialInspector;100;8;BlendGlowCenter;e1de45c0d41f68c41b2cc20c8b9c05ef;True;ForwardBase;0;0;ForwardBase;3;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;True;2;0;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=ForwardBase;True;2;0;;0;0;Standard;0;0;4;True;False;False;False;False;;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;71;1210.5,269.8124;Float;False;False;-1;2;ASEMaterialInspector;100;1;New Amplify Shader;e1de45c0d41f68c41b2cc20c8b9c05ef;True;ShadowCaster;0;3;ShadowCaster;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;;0;0;Standard;0;0
WireConnection;4;0;1;1
WireConnection;4;1;1;2
WireConnection;5;0;1;3
WireConnection;5;1;1;4
WireConnection;19;0;8;3
WireConnection;7;0;3;2
WireConnection;7;1;5;0
WireConnection;30;0;18;0
WireConnection;13;0;8;0
WireConnection;6;0;4;0
WireConnection;6;1;3;2
WireConnection;11;0;6;0
WireConnection;11;1;13;0
WireConnection;31;0;30;0
WireConnection;31;1;19;0
WireConnection;12;0;13;0
WireConnection;12;1;7;0
WireConnection;32;0;31;0
WireConnection;16;1;11;0
WireConnection;17;1;12;0
WireConnection;39;0;38;0
WireConnection;27;0;20;0
WireConnection;26;0;17;0
WireConnection;24;0;16;0
WireConnection;33;0;30;0
WireConnection;33;1;32;0
WireConnection;28;0;21;0
WireConnection;59;1;11;0
WireConnection;34;0;33;0
WireConnection;29;0;24;0
WireConnection;29;1;26;0
WireConnection;29;2;27;0
WireConnection;29;3;28;0
WireConnection;40;0;39;0
WireConnection;37;0;59;4
WireConnection;37;1;17;4
WireConnection;37;2;20;4
WireConnection;37;3;21;4
WireConnection;41;0;37;0
WireConnection;41;1;40;0
WireConnection;35;0;29;0
WireConnection;35;1;34;0
WireConnection;36;1;29;0
WireConnection;36;0;35;0
WireConnection;42;1;37;0
WireConnection;42;0;41;0
WireConnection;45;0;36;0
WireConnection;45;1;46;0
WireConnection;44;0;42;0
WireConnection;44;1;43;0
WireConnection;68;0;45;0
WireConnection;68;1;44;0
ASEEND*/
//CHKSM=D7B9FD54EFE554BFBF94E60A7BF31CD0C2B7AF97
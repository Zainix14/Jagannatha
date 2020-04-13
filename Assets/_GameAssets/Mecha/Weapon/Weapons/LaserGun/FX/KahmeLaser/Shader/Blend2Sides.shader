// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Blend2Sides"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.1
		[Toggle(_USECUSTOMDATA_ON)] _UseCustomData("Use Custom Data?", Float) = 0
		_SpeedMainTexUVNoise("SpeedMainTex U/V + Noise", Vector) = (0,0,0,0)
		_Noise("Noise", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_MainTex("Main Tex", 2D) = "white" {}
		_Emission("Emission", Float) = 2
		_BackFaceColor("BackFaceColor", Color) = (0,0.9138584,1,0)
		_Fresnel("Fresnel", Float) = 1
		_FresnelColor("FresnelColor", Color) = (0,0,0,0)
		_FresnelEmission("FresnelEmission", Float) = 1
		_FrontFaceColor("FrontFaceColor", Color) = (0,0,0,0)
		[Toggle(_USEFRESNEL_ON)] _UseFresnel("Use Fresnel?", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _USEFRESNEL_ON
		#pragma shader_feature _USECUSTOMDATA_ON
		#pragma surface surf Unlit keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float3 viewDir;
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float4 uv_tex4coord;
		};

		uniform float4 _FrontFaceColor;
		uniform float _Fresnel;
		uniform float _FresnelEmission;
		uniform float4 _FresnelColor;
		uniform float4 _BackFaceColor;
		uniform float _Emission;
		uniform sampler2D _MainTex;
		uniform float4 _SpeedMainTexUVNoise;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform sampler2D _Noise;
		uniform float _Cutoff = 0.1;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV36 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode36 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV36, _Fresnel ) );
			#ifdef _USEFRESNEL_ON
				float4 staticSwitch45 = ( ( _FrontFaceColor * ( 1.0 - fresnelNode36 ) ) + ( _FresnelEmission * _FresnelColor * fresnelNode36 ) );
			#else
				float4 staticSwitch45 = _FrontFaceColor;
			#endif
			float dotResult27 = dot( ase_worldNormal , i.viewDir );
			float4 lerpResult32 = lerp( staticSwitch45 , _BackFaceColor , (0.0 + (sign( dotResult27 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)));
			float2 appendResult2 = (float2(_SpeedMainTexUVNoise.x , _SpeedMainTexUVNoise.y));
			o.Emission = ( lerpResult32 * _Emission * i.vertexColor * i.vertexColor.a * tex2D( _MainTex, ( i.uv_texcoord + ( appendResult2 * _Time.y ) ) ) ).rgb;
			o.Alpha = 1;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float2 appendResult3 = (float2(_SpeedMainTexUVNoise.z , _SpeedMainTexUVNoise.w));
			#ifdef _USECUSTOMDATA_ON
				float staticSwitch12 = i.uv_tex4coord.z;
			#else
				float staticSwitch12 = 1.0;
			#endif
			clip( ( tex2D( _Mask, uv_Mask ) * tex2D( _Noise, ( (i.uv_tex4coord).xy + ( _Time.y * appendResult3 ) ) ) * staticSwitch12 ).r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
1229;171;1154;795;1286.806;896.3162;1.74701;True;False
Node;AmplifyShaderEditor.RangedFloatNode;37;-1968.128,-911.8581;Inherit;False;Property;_Fresnel;Fresnel;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;36;-1763.999,-988.267;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;1;-1868.848,42.85532;Inherit;False;Property;_SpeedMainTexUVNoise;SpeedMainTex U/V + Noise;2;0;Create;True;0;0;False;0;0,0,0,0;0,0,-3,-2;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-1800.046,-1456.541;Inherit;False;Property;_FresnelEmission;FresnelEmission;10;0;Create;True;0;0;False;0;1;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;42;-1416.164,-1583.783;Inherit;False;Property;_FrontFaceColor;FrontFaceColor;11;0;Create;True;0;0;False;0;0,0,0,0;0.9810126,1,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;41;-1440.979,-949.3161;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;38;-1709.407,-1242.147;Inherit;False;Property;_FresnelColor;FresnelColor;9;0;Create;True;0;0;False;0;0,0,0,0;1,0.8810505,0.3160377,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;25;-1386.483,-741.3787;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;26;-1384.678,-564.5355;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1428.778,-1209.029;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;3;-1543.263,331.6694;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DotProductOpNode;27;-1088.736,-640.3254;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1311.257,46.90661;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;6;-1563.23,40.79485;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-1223.102,-1210.772;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;2;-1548.21,-163.8134;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SignOpNode;28;-915.502,-629.4983;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;8;-1079.01,106.4962;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-1067.971,-1069.586;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-1378.35,343.3268;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1161.519,-290.7679;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1387.517,-139.5018;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-843.7086,398.3321;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-865.0993,262.3456;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;35;-601.4149,-806.9016;Inherit;False;Property;_BackFaceColor;BackFaceColor;7;0;Create;True;0;0;False;0;0,0.9138584,1,0;1,0.4967132,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;45;-595.6063,-978.9479;Inherit;False;Property;_UseFresnel;Use Fresnel?;12;0;Create;True;0;0;False;0;0;1;1;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;29;-597.2836,-633.2751;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-953.7198,-141.03;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;24;-337.3721,-237.8479;Inherit;True;Property;_MainTex;Main Tex;5;0;Create;True;0;0;False;0;-1;None;0227ebae070b26648bbe48b40df96e14;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;34;-225.1054,-508.178;Inherit;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-601.1151,190.3992;Inherit;True;Property;_Noise;Noise;3;0;Create;True;0;0;False;0;-1;None;aa537db5b03754b4a918586759703cf4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;22;-606.7176,-15.57951;Inherit;True;Property;_Mask;Mask;4;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;12;-593.1267,405.9719;Inherit;False;Property;_UseCustomData;Use Custom Data?;1;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-225.2701,-981.3311;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-275.632,-645.3219;Inherit;False;Property;_Emission;Emission;6;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-132.4089,71.86243;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;125.6254,-689.4893;Inherit;False;5;5;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;324.8437,-486.244;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Blend2Sides;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.1;True;False;0;True;Transparent;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;36;3;37;0
WireConnection;41;0;36;0
WireConnection;40;0;39;0
WireConnection;40;1;38;0
WireConnection;40;2;36;0
WireConnection;3;0;1;3
WireConnection;3;1;1;4
WireConnection;27;0;25;0
WireConnection;27;1;26;0
WireConnection;43;0;42;0
WireConnection;43;1;41;0
WireConnection;2;0;1;1
WireConnection;2;1;1;2
WireConnection;28;0;27;0
WireConnection;8;0;7;0
WireConnection;44;0;43;0
WireConnection;44;1;40;0
WireConnection;5;0;6;2
WireConnection;5;1;3;0
WireConnection;4;0;2;0
WireConnection;4;1;6;2
WireConnection;11;0;8;0
WireConnection;11;1;5;0
WireConnection;45;1;42;0
WireConnection;45;0;44;0
WireConnection;29;0;28;0
WireConnection;10;0;9;0
WireConnection;10;1;4;0
WireConnection;24;1;10;0
WireConnection;19;1;11;0
WireConnection;12;1;13;0
WireConnection;12;0;7;3
WireConnection;32;0;45;0
WireConnection;32;1;35;0
WireConnection;32;2;29;0
WireConnection;23;0;22;0
WireConnection;23;1;19;0
WireConnection;23;2;12;0
WireConnection;30;0;32;0
WireConnection;30;1;33;0
WireConnection;30;2;34;0
WireConnection;30;3;34;4
WireConnection;30;4;24;0
WireConnection;0;2;30;0
WireConnection;0;10;23;0
ASEEND*/
//CHKSM=F1B029FA1C717C790FFA2E6A78C892E36FDB5502
// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LaserTest"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_xSpeed("xSpeed", Float) = 2
		_intensity("intensity", Float) = 2
		_ySpeed("ySpeed", Float) = 0
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TextureSample2;
		uniform float4 _TextureSample2_ST;
		uniform sampler2D _TextureSample1;
		uniform float _xSpeed;
		uniform float _ySpeed;
		uniform sampler2D _TextureSample0;
		uniform float _intensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			float4 tex2DNode18 = tex2D( _TextureSample2, uv_TextureSample2 );
			float2 appendResult10 = (float2(_xSpeed , _ySpeed));
			float2 temp_output_7_0 = ( i.uv_texcoord + ( _Time.y * appendResult10 ) );
			float4 tex2DNode5 = tex2D( _TextureSample0, temp_output_7_0 );
			float4 color12 = IsGammaSpace() ? float4(0.6132076,0.6132076,0.6132076,0) : float4(0.3341808,0.3341808,0.3341808,0);
			o.Emission = ( ( tex2DNode18 * tex2D( _TextureSample1, temp_output_7_0 ) * tex2DNode5 ) * i.vertexColor * color12 * _intensity ).rgb;
			o.Alpha = tex2DNode5.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
1255;411;850;445;1187.474;1276.683;3.542701;True;False
Node;AmplifyShaderEditor.RangedFloatNode;9;-768.7174,90.88431;Inherit;False;Property;_xSpeed;xSpeed;1;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-766.7174,196.8843;Inherit;False;Property;_ySpeed;ySpeed;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;4;-474.1244,-36.35092;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-548.1244,85.64908;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-291.1244,62.64908;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-382.772,-363.5094;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-84.43488,-140.4754;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;18;22.61172,-950.0699;Inherit;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;0;0;False;0;-1;7dc4c564a2e1e6e43a0915b440430bef;592fcd081ad17a941be5170b633a1bf0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;41.87664,-595.049;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;-1;592fcd081ad17a941be5170b633a1bf0;592fcd081ad17a941be5170b633a1bf0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;69.70988,-304.4537;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;23e606090dcfbc44ea10669da683802e;23e606090dcfbc44ea10669da683802e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;11;237.3069,-4.336243;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;12;25.27536,192.2186;Inherit;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.6132076,0.6132076,0.6132076,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;395.907,292.9468;Inherit;True;Property;_intensity;intensity;2;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;509.8668,-506.1918;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;637.5124,-203.047;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;809.3206,-472.8893;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;16;877.1433,-226.1269;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;LaserTest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;1;0;4;0
WireConnection;1;1;10;0
WireConnection;7;0;6;0
WireConnection;7;1;1;0
WireConnection;17;1;7;0
WireConnection;5;1;7;0
WireConnection;19;0;18;0
WireConnection;19;1;17;0
WireConnection;19;2;5;0
WireConnection;2;0;19;0
WireConnection;2;1;11;0
WireConnection;2;2;12;0
WireConnection;2;3;14;0
WireConnection;20;0;18;4
WireConnection;20;1;5;4
WireConnection;16;2;2;0
WireConnection;16;9;5;4
ASEEND*/
//CHKSM=D17073F50B316D13DDBAC1642B7822F607D285C6
// Uses geometry normals to distort the image behind, and
// an additional texture to tint the color.

Shader ".water/refraction"
{
   Properties 
   {
      _EtaRatio ("EtaRatio", Float)  = 1.33
      _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	  _Colour ("Colour", Color) = (1,1,1,1)
 
	  _BumpMap ("Noise text", 2D) = "bump" {}
	  _Magnitude ("Magnitude", Range(0,1)) = 0.05
	  _Offset("offset", Float) = 1
	  _WaterPeriod("Water Period", Float) = 1
   }
   SubShader 
   {
     Tags{ "RenderType"="Opaque"}
   	 LOD 100

     Pass 
     {   
     	CGPROGRAM
        	#pragma vertex vert  
        	#pragma fragment frag

        	#include "UnityCG.cginc"

        	sampler2D _GrabTexture;
 
			sampler2D _MainTex;
			fixed4 _Colour;
 
			sampler2D _BumpMap;
			float  _Magnitude;
			float _Offset;
			float _WaterPeriod;

        	struct vertexInput
        	{
        		float4 position : POSITION;
        		float2 texCoord: TEXCOORD0;
        		float3 normal   : NORMAL;
        	};

        	struct vertexOutput
        	{
        		float4 oPosition : POSITION;
        		float2 oTexCoord : TEXCOORD0;
        		float3 T         : TEXCOORD1;
        	};

        	float etaRatio = 1.33f;

			vertexOutput vert(vertexInput i)
			{
				vertexOutput o;
				o.oPosition = mul(UNITY_MATRIX_MVP , i.position);
				o.oTexCoord = i.texCoord;

				float3 positionW = mul(_Object2World, i.position).xyz;
				float3 N = mul((float3x3)_Object2World, i.normal);
				N = normalize(N);

				// Compute the incident and refracted vectors
  				float3 I = normalize (positionW * _WorldSpaceCameraPos);

  				o.T = refract(I, N, etaRatio);

				return o;
			}

		    float2 sinusoid (float2 x, float2 m, float2 M, float2 p) 
			{
				float2 e   = M - m;
				float2 c = 3.1415 * 2.0 / p;
				return e / 2.0 * (1.0 + sin(x * c)) + m;
			}

			// Fragment function
			fixed4 frag (vertexOutput i) : COLOR 
			{
				fixed4 noise = tex2D(_BumpMap, i.oTexCoord);
				fixed4 mainColour = tex2D(_MainTex, i.oTexCoord);
			
				float time = _Time[1];
 
				float2 waterDisplacement =
				sinusoid
				(
					float2 (time, time) + (noise.xy) * _Offset,
					float2(-_Magnitude, -_Magnitude),
					float2(+_Magnitude, +_Magnitude),
					float2(_WaterPeriod, _WaterPeriod)
				);
				
				i.T.xy += waterDisplacement;
				float2 uv = i.T.xy;
				fixed4 col = tex2D( _GrabTexture, uv);
				fixed4 causticColour = tex2D(_BumpMap, i.oTexCoord.xy*0.25 + waterDisplacement*5);
				return col * mainColour * _Colour * causticColour;
				}


         ENDCG
      }
   }
}

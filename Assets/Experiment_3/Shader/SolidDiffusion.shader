// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "BitshiftProgrammer/Liquid"
{
     Properties
    {
		_TopColor ("TopColour", Color) = (1,1,1,1)
        _BottomColor("BottomColor",Color) = (1,1,1,1)
        _Center("Center",Float)= 0.5
        _Diffusion ("Diffusion", Range(0,10)) = 0
        _AmbientLight("AmbientLight",Color) = (1,1,1,1)
        _Gloss("Gloss",Range(1,100)) = 1
        _Roughness("Roughness", Range(0.0, 10.0)) = 0.0
    }
 
    SubShader
    {
        Tags {"RenderType"="Opaque"}
        LOD 100
        Zwrite on       
        Cull off
        
        Pass
        {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #include "UnityCG.cginc"
 
         struct appdata
         {
           float4 vertex : POSITION;
		   float3 normal : NORMAL;
		   float2 uv0: TEXCOORD0;
         };
 
         struct v2f
         {
            float4 clipSpacePos:SV_POSITION;
            float3 localPos:float3;
            float2 uv0:TEXCOORD0;
            float3 normal:TEXCOORD1;
			float3 worldPos:TEXCOORD2;
			float3 worldNormal:NORMAL;
         };
 
         sampler2D _MainTex;
         float4 _MainTex_ST;
         float _Diffusion;
         float _Center;
         float4 _TopColor, _BottomColor,_AmbientLight;
         float _Gloss;
		 uniform float4 _LightColor0 = {1,1,1,1};
		 float _Roughness;
			
         v2f vert (appdata v)
         {
            v2f o;
            o.uv0 = v.uv0;
            o.normal = v.normal;
			o.worldPos = mul(unity_ObjectToWorld,v.vertex);
            o.clipSpacePos = UnityObjectToClipPos(v.vertex);
            o.localPos = v.vertex;
            o.worldNormal = UnityObjectToWorldNormal(v.normal);
            return o;
         } 
         
         fixed4 frag (v2f i) : COLOR
         {
            //float4 color = diffusionColor(i.localPos.y);
            float diff = _Diffusion/2;
            float t = smoothstep(_Center - diff, _Center + diff, i.localPos.y);
            float3 color = lerp(_BottomColor, _TopColor, t);
            
            float3 normal = normalize(i.normal);
            
            //Lighting
            float3 lightDir = _WorldSpaceLightPos0.xyz;
            float3 lightColor = _LightColor0.rgb;
            
            //Direct diffuse light
            float lightFalloff = max(0, dot(lightDir, normal));
            float3 directDiffuseLight = lightColor * lightFalloff;
            
            //Ambient Light
            float3 ambientLight = _AmbientLight;
       
            //Direct specular light
            float3 camPos = _WorldSpaceCameraPos;
            float3 fragToCam = camPos - i.worldPos;
            float3 viewDir = normalize(fragToCam);
            float3 viewReflect = reflect(-viewDir, normal);
            float specularFalloff = max(0,dot(viewReflect, lightDir));
            specularFalloff = pow(specularFalloff, _Gloss);
            
            float3 directSpecular = specularFalloff * lightColor;
            
            
            //Reflection
            half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos)); //Direction of ray from the camera towards the object surface
            half3 reflection = reflect(-worldViewDir, i.worldNormal); // Direction of ray after hitting the surface of object
            /*If Roughness feature is not needed : UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, reflection) can be used instead.
            It chooses the correct LOD value based on camera distance*/
            half4 skyData = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, reflection, _Roughness); //UNITY_SAMPLE_TEXCUBE_LOD('cubemap', 'sample coordinate', 'map-map level')
            half3 skyColor = DecodeHDR (skyData, unity_SpecCube0_HDR); // This is done because the cubemap is stored HDR
            
            
            //Composite
            float3 diffuseLight = ambientLight + directDiffuseLight;
            float3 finalSurfaceLight = diffuseLight * color.xyz * skyColor + directSpecular;
            
		    return float4(finalSurfaceLight,1);
         }
         
         ENDCG
        }
        Pass
		{
			Tags{"LightMode"="ShadowCaster"}
			Blend One Zero
			CGPROGRAM 
			#pragma target 3.0 
			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc"
			float4 vert(float4 vertex:POSITION, float3 normal:NORMAL):SV_POSITION
			{
				float4 clipPos = UnityClipSpaceShadowCasterPos(vertex.xyz,normal);
				return UnityApplyLinearShadowBias(clipPos);
			}

			float4 frag():SV_Target
			{
				return 0;
			}
			ENDCG
		}
		
    }
}
// Upgrade NOTE: replaced 'PositionFog()' with transforming position into clip space.
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Glass Shader" {
        Properties {
                _Color ("Main Color", Color) = (1,1,1,1)
                _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
                _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
                _Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
                _ReflectStrength( "Reflection Strength" , Range(0,1)) = 0.5
        }
        
Category {
        Tags {Queue=Transparent}
        Alphatest Greater 0
        ZWrite Off
        ColorMask RGB
        
        // ------------------------------------------------------------------
        // ARB fragment program
        
        #warning Upgrade NOTE: SubShader commented out; uses Unity 2.x per-pixel lighting. You should rewrite shader into a Surface Shader.
#warning Upgrade NOTE: SubShader commented out; uses Unity 2.x per-pixel lighting. You should rewrite shader into a Surface Shader.
#warning Upgrade NOTE: SubShader commented out; uses Unity 2.x per-pixel lighting. You should rewrite shader into a Surface Shader.
/*SubShader {
                // Ambient pass
                Pass {
                        Name "BASE"
                        Tags {"LightMode" = "Always" /* Upgrade NOTE: changed from PixelOrNone to Always */}
                        Fog { Color [_AddFog] }
                        Blend SrcAlpha OneMinusSrcAlpha
                        Color [_PPLAmbient]
                        SetTexture [_MainTex] {constantColor [_Color] Combine texture * primary DOUBLE, texture * primary}
                }
                // Vertex lights
                Pass { 
                        Name "BASE"
                        Tags {"LightMode" = "Vertex"}
                        Fog { Color [_AddFog] }
                        Blend SrcAlpha OneMinusSrcAlpha
                        Lighting On
                        Material {
                                Diffuse [_Color]
                                Emission [_PPLAmbient]
                        } 
                        SetTexture [_MainTex] {combine texture * primary DOUBLE, texture * primary}
                }
                // Pixel lights
                Pass {  
                        Name "PPL"
                        Tags { "LightMode" = "Pixel" }
                        Blend SrcAlpha One
                        Fog { Color [_AddFog] }

CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv,normal,lightDir,reflection)
#pragma exclude_renderers d3d11
#pragma fragment frag
#pragma vertex vert
#pragma multi_compile_builtin_noshadows
#pragma fragmentoption ARB_fog_exp2
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"
#include "AutoLight.cginc"

struct v2f {
        float4 pos : SV_POSITION;
        LIGHTING_COORDS
        float2  uv;
        float3  normal;
        float3  lightDir;
        float3    reflection;
};

uniform float4 _MainTex_ST;

v2f vert (appdata_base v)
{
        v2f o;
        o.pos = UnityObjectToClipPos (v.vertex);
        o.normal = v.normal;
        o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
        o.lightDir = ObjSpaceLightDir( v.vertex );
        float3 viewDir = ObjSpaceViewDir( v.vertex );
        float3 reflection = reflect( -viewDir,v.normal);
        o.reflection = mul( (float3x3)unity_ObjectToWorld , reflection);
        TRANSFER_VERTEX_TO_FRAGMENT(o);
        return o;
}

uniform sampler2D _MainTex;
uniform float4 _Color;
uniform samplerCUBE _Cube;
uniform float4 _ReflectColor;
uniform float _ReflectStrength;

float4 frag (v2f i) : COLOR
{
        half4 texcol = tex2D( _MainTex, i.uv ); 
        half4 c = DiffuseLight( i.lightDir, i.normal, texcol, LIGHT_ATTENUATION(i) );
        c.a = texcol.a * _Color.a;
        
        half4 reflectCol = texCUBE( _Cube , i.reflection );
        reflectCol *= _ReflectColor;
        reflectCol *= _ReflectStrength;
        
        c.rgb *= reflectCol.rgb;
        
        return c;
}
ENDCG

                        SetTexture [_MainTex] {combine texture}
                        SetTexture [_LightTexture0] {combine texture}
                        SetTexture [_LightTextureB0] {combine texture}
                }
        }*/
}
        
        FallBack "Transparent/Diffuse"
}
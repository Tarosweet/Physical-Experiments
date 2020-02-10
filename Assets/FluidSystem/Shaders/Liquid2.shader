// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,5,fgcg:0,5,fgcb:0,5,fgca:1,fgde:0,01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:36680,y:34050,varname:node_3138,prsc:2|emission-7445-RGB,alpha-7445-A,clip-2336-OUT;n:type:ShaderForge.SFN_Time,id:5325,x:34700,y:33442,varname:node_5325,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3365,x:34901,y:33432,varname:node_3365,prsc:2|A-5325-T,B-7192-OUT;n:type:ShaderForge.SFN_Vector1,id:7192,x:34714,y:33725,varname:node_7192,prsc:2,v1:2;n:type:ShaderForge.SFN_Sin,id:7149,x:35082,y:33407,varname:node_7149,prsc:2|IN-3365-OUT;n:type:ShaderForge.SFN_Vector1,id:8018,x:34932,y:33716,varname:node_8018,prsc:2,v1:0,1;n:type:ShaderForge.SFN_Multiply,id:3178,x:35148,y:33682,varname:node_3178,prsc:2|A-7149-OUT,B-8018-OUT,C-8165-OUT;n:type:ShaderForge.SFN_ObjectPosition,id:8513,x:34976,y:33901,varname:node_8513,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:3285,x:34967,y:34120,varname:node_3285,prsc:2;n:type:ShaderForge.SFN_Subtract,id:8165,x:35142,y:34011,varname:node_8165,prsc:2|A-8513-XYZ,B-3285-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:325,x:35335,y:34041,varname:node_325,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-8165-OUT;n:type:ShaderForge.SFN_Add,id:9431,x:35544,y:33924,varname:node_9431,prsc:2|A-3178-OUT,B-325-OUT;n:type:ShaderForge.SFN_OneMinus,id:511,x:35436,y:34232,varname:node_511,prsc:2|IN-5-OUT;n:type:ShaderForge.SFN_Subtract,id:6806,x:35657,y:34208,varname:node_6806,prsc:2|A-9431-OUT,B-511-OUT;n:type:ShaderForge.SFN_Vector1,id:8898,x:35657,y:34455,varname:node_8898,prsc:2,v1:0,5;n:type:ShaderForge.SFN_Divide,id:3161,x:35856,y:34287,varname:node_3161,prsc:2|A-6806-OUT,B-8898-OUT;n:type:ShaderForge.SFN_Clamp01,id:7923,x:36069,y:34287,varname:node_7923,prsc:2|IN-3161-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2336,x:36257,y:34367,varname:node_2336,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-7923-OUT;n:type:ShaderForge.SFN_Slider,id:5,x:35081,y:34370,ptovrint:False,ptlb:node_5,ptin:_node_5,varname:_node_5,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:1,185536,max:10;n:type:ShaderForge.SFN_FragmentPosition,id:805,x:35822,y:34078,varname:node_805,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:7445,x:36221,y:34014,ptovrint:False,ptlb:node_7445,ptin:_node_7445,varname:_node_7445,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4012-OUT,MIP-805-Y;n:type:ShaderForge.SFN_TexCoord,id:9841,x:35759,y:33685,varname:node_9841,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:4012,x:36150,y:33723,varname:node_4012,prsc:2|A-9841-U,B-4739-OUT;n:type:ShaderForge.SFN_Add,id:1948,x:35771,y:33889,varname:node_1948,prsc:2|A-805-XYZ,B-3161-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4739,x:35958,y:33768,varname:node_4739,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-1948-OUT;proporder:5-7445;pass:END;sub:END;*/

Shader "Shader Forge/Liquid2" {
    Properties {
        _node_5 ("node_5", Range(-10, 10)) = 1.185536
        _node_7445 ("node_7445", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0
            uniform sampler2D _node_7445; uniform float4 _node_7445_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 node_5325 = _Time;
                float3 node_8165 = (objPos.rgb-i.posWorld.rgb);
                float _node_5_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5 );
                float3 node_3161 = ((((sin((node_5325.g*2,0))*0,1*node_8165)+node_8165.g)-(1.0 - _node_5_var))/0,5);
                clip(saturate(node_3161).r - 0.5);
////// Lighting:
////// Emissive:
                float2 node_4012 = float2(i.uv0.r,(i.posWorld.rgb+node_3161).g);
                float4 _node_7445_var = tex2Dlod(_node_7445,float4(TRANSFORM_TEX(node_4012, _node_7445),0.0,i.posWorld.g));
                float3 emissive = _node_7445_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,_node_7445_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float4 node_5325 = _Time;
                float3 node_8165 = (objPos.rgb-i.posWorld.rgb);
                float _node_5_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5 );
                float3 node_3161 = ((((sin((node_5325.g*2,0))*0,1*node_8165)+node_8165.g)-(1.0 - _node_5_var))/0,5);
                clip(saturate(node_3161).r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

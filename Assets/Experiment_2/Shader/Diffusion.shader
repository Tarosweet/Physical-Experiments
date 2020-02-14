Shader "Unlit/Diffusion"
{
     Properties
    {
		_Colour ("Colour", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(-10,10)) = 0.0
		[HideInInspector] _WobbleX ("WobbleX", Range(-1,1)) = 0.0
		[HideInInspector] _WobbleZ ("WobbleZ", Range(-1,1)) = 0.0
    }
 
    SubShader
    {
        Tags {"RenderType"="Transparent" "Queue"="Transparent" "DisableBatching" = "True" "IgnoreProjector" = "True"}
        LOD 100
        Zwrite off          
        Blend SrcAlpha OneMinusSrcAlpha
        Cull off // we want the front and back faces
        AlphaToMask off
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
		   float2 uv: TEXCOORD0;
         };
 
         struct v2f
         {
            float4 vertex : SV_POSITION;
			float3 viewDir : COLOR;
		    float3 normal : COLOR2;
			float fillEdge : TEXCOORD2;
			float3 worldPos: float3;
         };
 
         sampler2D _MainTex;
         float4 _MainTex_ST;
         float _FillAmount, _WobbleX, _WobbleZ;
         float4 _TopColor, _FoamColor;
         float4 _Colour;
         float4 _Color[15];
         bool _Diffusion[15];
         float _RangeDiffusion[15];
         float _Heights[17];
         int _Count;
           
		 float4 RotateAroundYInDegrees (float4 vertex, float degrees)
         {
			float alpha = degrees * UNITY_PI / 180;
			float sina, cosa;
			sincos(alpha, sina, cosa);
			float2x2 m = float2x2(cosa, sina, -sina, cosa);
			return float4(vertex.yz , mul(m, vertex.xz)).xzyw ;
         }

         v2f vert (appdata v)
         {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            float3 worldPos = mul (unity_ObjectToWorld, v.vertex.xyz);
			// rotate it around XY
			float3 worldPosX = RotateAroundYInDegrees(float4(worldPos,0),360);
			// rotate around XZ
			float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);
			// combine rotations with worldPos, based on sine wave from script
			float3 worldPosAdjusted = worldPos + (worldPosX  * _WobbleX)+ (worldPosZ* _WobbleZ); 
			// how high up the liquid is
			o.fillEdge =  worldPosAdjusted.y + _FillAmount;
			o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
            o.normal = v.normal;
            o.worldPos = worldPosAdjusted;
            return o;
         }      
         
         float4 CalcColor(float pos){
            for(int i = 0; i < _Count; i++){
                if(pos > _Heights[i] && pos <= _Heights[i + 1]){
                uint k = i / 2;
                        float dist = _Heights[i+1] - _Heights[i];
                        float posOne = (pos - _Heights[i]) / dist;
                        float diff = 0.5 - _RangeDiffusion[k] / 2;
                        if(posOne < diff && i > 0){
                            return lerp((_Color[i] + _Color[i-1]) / 2, _Color[i], posOne * (1 / diff));
                        } 
                        else{
                        diff = (0.5 + _RangeDiffusion[i] / 2);
                            if(posOne >= 1 - diff && i < _Count - 1){
                                return lerp(_Color[i],(_Color[i] + _Color[i + 1]) / 2,  (posOne - diff) * (1 / diff));
                            }
                        }
                        return _Color[i];
                }
            }
            return _Color[0];
         }
         
         fixed4 frag (v2f i, fixed facing : VFACE) : COLOR
         {
         _RangeDiffusion[0] = 0;
         _RangeDiffusion[1] = 0;
         _RangeDiffusion[2] = 0;
         _RangeDiffusion[3] = 0;
         _RangeDiffusion[4] = 0;
         _RangeDiffusion[5] = 0;
           float4 color = CalcColor(i.worldPos.y);
		   float4 result = step(i.fillEdge, 0.5);
           float4 resultColored = result *  color;
		   return resultColored;
         }
         ENDCG
        }
    }
}

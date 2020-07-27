Shader "Unlit/001"
{
	//属性块
    Properties
    {
		_Int("Int",Int) = 2
		_Float("Float",Float) = 1.5
		_Range("Range",Range(0.0,2.0)) = 1.0
		_Color("Color",Color)=(0,0,0,0)
		_Vector("Vector",Vector) = (1,4,3,8)
        _MainTex ("Texture", 2D) = "white" {}
		//_MainTex 属性的名字，简单说就是变量名，在之后整个Shader代码中将使用这个名字来获取该属性的内容
		//"Texture" 这个字符串将显示在Unity的材质编辑器中作为Shader的使用者可读的内容
		// 2D 这个属性的类型
		// "white" 值
		_Cube("Cube",Cube) = "white"{}
		_3D("3D",3D) = "black"{}
    }
    SubShader
    {
        Tags // 标签(可选，可写在Pass内部)
		{ 
			"Queue" = "Transparent"//渲染顺序
			"RenderType"="Opaque" //着色器替换功能
			"DisbaleBatching" = "True" //是否进行合批
			"ForceNoShadowCasting" = "True" //是否投射阴影
			"IgnoreProjector" = "True"  //受不受Projector的影响，通常用于透明物体
			"CanUseSprite" = "False" // 是否用于图片的Shader，通常用于UI
			"PreviewType" = "Plane" // 用于shader面板预览类型
		}

		//Render设置（可选，可写在Pass内部)）
		//Cull off/back/front //选择渲染面
		//ZTest Always/Less/Greater/LEqual/GEqual/EqualNotEqual //深度测试
		//Zwrite off/on //深度写入
		//Blend SrcFactor DstFactor //混合
        LOD 100 //多层次细节

        Pass
        {
			Name "Default"
			Tags // 单独定义
			{
				"LightMode" = "ForwardBase" //定义该Pass通道在unity渲染流水中角色
				"RequireOptions" = "SoftVegetation"//满足某些条件才能渲染
			}


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }

	//Fallback "Transparent/Cutout/VertexLit" //如果所有子着色器都无法在该硬件上运行时使用
}

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33396,y:32613,varname:node_3138,prsc:2|emission-9933-OUT;n:type:ShaderForge.SFN_Tex2d,id:2927,x:31828,y:32600,ptovrint:False,ptlb:Normals,ptin:_Normals,varname:_Normals,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9292,x:31967,y:32948,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:179,x:32292,y:32854,varname:node_179,prsc:2|A-9292-G,B-9292-G,C-135-OUT;n:type:ShaderForge.SFN_Vector1,id:135,x:32168,y:33013,varname:node_135,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5834,x:32325,y:32683,varname:node_5834,prsc:2|A-5824-OUT,B-179-OUT;n:type:ShaderForge.SFN_RemapRange,id:5824,x:32020,y:32600,varname:node_5824,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-2927-RGB;n:type:ShaderForge.SFN_RemapRange,id:9933,x:32942,y:32794,varname:node_9933,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-4289-OUT;n:type:ShaderForge.SFN_Normalize,id:4289,x:32645,y:32812,varname:node_4289,prsc:2|IN-5834-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5254,x:32518,y:32572,varname:node_5254,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5834-OUT;n:type:ShaderForge.SFN_Dot,id:2955,x:32761,y:32545,varname:node_2955,prsc:2,dt:0|A-5254-OUT,B-5254-OUT;n:type:ShaderForge.SFN_OneMinus,id:7076,x:32950,y:32535,varname:node_7076,prsc:2|IN-2955-OUT;n:type:ShaderForge.SFN_Sqrt,id:2326,x:33143,y:32562,varname:node_2326,prsc:2|IN-7076-OUT;n:type:ShaderForge.SFN_Append,id:161,x:33292,y:32444,varname:node_161,prsc:2|A-5254-OUT,B-2326-OUT;proporder:2927-9292;pass:END;sub:END;*/

Shader "Shader Forge/BlendShader" {
    Properties {
        _Normals ("Normals", 2D) = "white" {}
        _Intensity ("Intensity", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform sampler2D _Intensity; uniform float4 _Intensity_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Normals_var = tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals));
                float4 _Intensity_var = tex2D(_Intensity,TRANSFORM_TEX(i.uv0, _Intensity));
                float3 node_5834 = ((_Normals_var.rgb*2.0+-1.0)*float3(_Intensity_var.g,_Intensity_var.g,1.0));
                float3 emissive = (normalize(node_5834)*0.5+0.5);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

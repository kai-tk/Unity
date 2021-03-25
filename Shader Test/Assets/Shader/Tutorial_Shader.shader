Shader "Unlit/Tutorial_Shader"{

	//Unityからパラメータを取得
	Properties{
		_Color("Colour", Color) = (1,1,1,1)
		_MainTexture("Main Texture",2D) = "white"{}
		_DissolveTexture("Dissolve Texture",2D) = "white"{}
		_DissolveCutoff("Dissolve CutOff",Range(0,1))=1
	}

	//シェーダー
	SubShader{
		//オブジェクトのレンダリング
		Pass{
			CGPROGRAM
				//関数名
				#pragma vertex vertexFunction
				#pragma fragment fragmentFunction

				#include "UnityCG.cginc"

				//シェーダーを適用するモデル
				struct appdata{
					float4 vertex : POSITION; //頂点座標
					float2 uv : TEXCOORD0; //uv座標
				};

				//描画に必要な情報を持つフラグメントの頂点モデル
				struct v2f{
					float4 position : SV_POSITION; //頂点システム値
					float2 uv : TEXCOORD0; //uv座標
				};

				float4 _Color;
				sampler2D _MainTexture;
				sampler2D _DissolveTexture;
				float _DissolveCutoff;

				//入力データからv2fを計算・確定
				v2f vertexFunction(appdata IN) {
					v2f OUT;
					//入力データの頂点座標をカメラのクリップ空間に変換して出力データの位置に渡す
					OUT.position=UnityObjectToClipPos(IN.vertex);
					OUT.uv = IN.uv;
					return OUT;
				}

				//v2fをもとにピクセル描画
				fixed4 fragmentFunction(v2f IN) : SV_TARGET{
					float4 textureColor = tex2D(_MainTexture, IN.uv);
					float4 dissolveColor = tex2D(_DissolveTexture, IN.uv);
					//dissolveTextureからCutOff値を引いて0になったピクセル(にあるMainTexture)を非表示
					clip(dissolveColor.rgb - _DissolveCutoff);
					return textureColor;
					// tex2D(_MainTexture, IN.uv);
					// return _Color;
				}
			ENDCG
		}
	}
}

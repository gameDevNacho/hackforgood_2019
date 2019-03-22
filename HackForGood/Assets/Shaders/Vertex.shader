Shader "Custom/HappyShader"
{
	Properties{
	 _MainTex("Texture", 2D) = "white" {}
	 _HorizontalAmount("Horizontal Amount", float) = 0.1
	 _VerticalAmount("Vertical Amount", float) = 0.1
		 _Speed("Speed", float) = 40
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  CGPROGRAM
		  #pragma surface surf Lambert vertex:vert
		  struct Input {
			  float2 uv_MainTex;
		  };
	 float _HorizontalAmount;
		  float _VerticalAmount;
		  float _Speed;

		  void vert(inout appdata_full v) {
			  v.vertex.y += _VerticalAmount * cos(_Time.x * _Speed) * v.vertex.y;
			  v.vertex.z += _HorizontalAmount * cos(_Time.x * 0.5 * _Speed) * v.vertex.z;
		  }
		  sampler2D _MainTex;
		  void surf(Input IN, inout SurfaceOutput o) {
			  o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		  }
		  ENDCG
	 }
		 Fallback "Diffuse"
}
// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "CustomGlobalShapeShader" {
Properties {
	_MainTex ("Primary Texture", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_Color ("Main Color", Color) = (1,0,0,1)
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 100
	
	Pass {
		Lighting Off
		Alphatest Greater [_Cutoff]
        SetTexture [_SecondaryTex]
        SetTexture [_MainTex]
        {
            constantColor [_Color]
            combine texture * constant,
            texture
        }
	}
}
}
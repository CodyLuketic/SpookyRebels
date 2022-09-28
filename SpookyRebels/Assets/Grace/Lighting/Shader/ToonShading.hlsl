
void ToonShading_float(in float3 Normal, in float ToonRampSmoothness, 
	in float3 ClipSpacePos , in float3 worldPos, in float4 ToonRampTinting,
	int float ToonRampOffset, out float3 ToonRampOutput, out float3 Direction)
	{
		#ifdef SHADERGRAPH_PREVIEW
		ToonRampOutput = float(0,5,0,5,0);
		Direction = float3(0,5,0,5,0);
		#else
			#if SHADOWS_SCREEN
				half4 shadowCoord = ComputerScreenPos(ClipSpacePos);
			#else
				half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
			#endif

			#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS_CASCADE
				Light light = GetMainLight (shadowCoord)
			#endif

			half d = dot(Normal, light.direction) * 0.5 + 0.5;

			half toonRamp = smoothstep(ToonRampOffset, ToonRampOffset + ToonRampSmoothness, d);

			toonRamp *= light.shadowAttenuation; 


	}
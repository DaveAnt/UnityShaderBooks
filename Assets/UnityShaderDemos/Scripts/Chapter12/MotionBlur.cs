using UnityEngine;
using System.Collections;

public class MotionBlur : PostEffectsBase {

	public Shader motionBlurShader;
	private Material motionBlurMaterial = null;

	public Material material {  
		get {
			motionBlurMaterial = CheckShaderAndCreateMaterial(motionBlurShader, motionBlurMaterial);
			return motionBlurMaterial;
		}  
	}

	[Range(0.0f, 0.9f)]
	public float blurAmount = 0.5f;

	[Range(1, 10)]
	public int blurSpeed = 1;

	private int frameCount = 0;

	private RenderTexture accumulationTexture;

	void OnDisable() {
		DestroyImmediate(accumulationTexture);
	}

	void OnRenderImage (RenderTexture src, RenderTexture dest) {
		if (material != null) {
			// Create the accumulation texture
			if (accumulationTexture == null || accumulationTexture.width != src.width || accumulationTexture.height != src.height)
			{
				DestroyImmediate(accumulationTexture);
				accumulationTexture = new RenderTexture(src.width, src.height, 0);
				accumulationTexture.hideFlags = HideFlags.HideAndDontSave;
				Graphics.Blit(src, accumulationTexture);
				Graphics.Blit(src, dest);
			}
			else {
				material.SetFloat("_BlurAmount", 1.0f - blurAmount);
				Graphics.Blit(accumulationTexture, dest, material);

				frameCount += 1;
				if (frameCount > blurSpeed)
				{
					Graphics.Blit(src, accumulationTexture, material);
					frameCount = 0;
				}
			}	
		} else {
			Graphics.Blit(src, dest);
		}
	}
}

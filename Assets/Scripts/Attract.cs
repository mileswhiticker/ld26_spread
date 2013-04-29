using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour
{
	float tLeftNextFrame = 0.3f;
	float tPerFrame = 0.3f;
	int curFrame = 0;
	int numFrames = 3;
	
	Vector2 scale;
	public Texture2D attract;
	public Texture2D thinking;
	
	void Start()
	{
		scale = new Vector2(1.0f / (float)numFrames, 1);
		this.gameObject.renderer.material.SetTextureScale("_MainTex", scale);
		this.gameObject.renderer.material.mainTexture.filterMode = FilterMode.Point;
		
		attract = (Texture2D)Resources.Load("attract");
		thinking = (Texture2D)Resources.Load("attract_thinking");
	}
	
	void Update()
	{
		if(tLeftNextFrame <= 0)
		{
			tLeftNextFrame = tPerFrame;
			if(++curFrame >= numFrames)
			{
				curFrame = 0;
			}
			//
			Vector2 offset = new Vector2(curFrame * scale.x, 0);
			this.gameObject.renderer.material.SetTextureOffset("_MainTex", offset);
		}
		else
		{
			tLeftNextFrame -= Time.deltaTime;
		}
	}
}

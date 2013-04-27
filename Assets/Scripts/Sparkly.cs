using UnityEngine;
using System.Collections;

public partial class Sparkly : MonoBehaviour
{
	public float tPerFrame = 0.1f;
	public float tLeftNextFrame = 0.1f;
	public int curFrame = 0;
	public int numFrames = 3;
	
	Vector2 scale;
	
	void Start()
	{
		scale = new Vector2(1.0f / (float)numFrames, 1);
		this.gameObject.renderer.material.SetTextureScale("_MainTex", scale);
	}
	
	void Update()
	{
		tLeftNextFrame -= Time.deltaTime;
		if(tLeftNextFrame <= 0)
		{
			tLeftNextFrame = tPerFrame;
			if(++curFrame >= numFrames)
			{
				curFrame = 0;
			}
			//Debug.Log("frame: " + curFrame);
			
			Vector2 offset = new Vector2(curFrame * scale.x, 0);
			this.gameObject.renderer.material.SetTextureOffset("_MainTex", offset);
		}
	}
}

using UnityEngine;
using System.Collections;

public partial class Splotch : MonoBehaviour
{
	public bool isMachine = false;
	public void Init(bool a_IsMachine = true)
	{
		int num = (int)Mathf.Ceil(Random.value * 3);
		//Texture curTex = this.renderer.material.GetTexture("_MainTex");
		if(a_IsMachine)
		{
			this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("oil" + num);
			isMachine = true;
		}
		else
		{
			this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("grass" + num);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		//work out what we've hit
		GameObject gameObject = collision.gameObject;
		if(gameObject)
		{
			Splotch splotch = gameObject.GetComponent<Splotch>();
			if(splotch)
			{
				//friend or foe?
				if(splotch.isMachine != isMachine)
				{
					//self combust
					GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
					gameController.DestroyGameObject(gameObject);
					gameController.DestroyGameObject(this.gameObject);
				}
			}
		}
	}
}

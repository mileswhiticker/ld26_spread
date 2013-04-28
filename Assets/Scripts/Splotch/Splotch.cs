using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Splotch : MonoBehaviour
{
	public bool isMachine = false;
	public int splotchLevel = 1;
	public float tLeftUntilMerge = 0.0f;
	GameController gameController;
	public List<GameObject> overlappingSplotchGameObjects;
	public bool ignoreMe = false;
	
	void Start()
	{
		overlappingSplotchGameObjects = new List<GameObject>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update()
	{
		if(tLeftUntilMerge > 0)
		{
			tLeftUntilMerge -= Time.deltaTime;
		}
		
		/*for(int index=0;index<overlappingSplotchGameObjects.Count;++index)
		{
			GameObject gameObject = overlappingSplotchGameObjects[index];
			if(gameObject)
			{
				//drift away from them
				Vector3 newForce = (this.transform.position - gameObject.transform.position).normalized * 5;
				this.rigidbody.AddForce(newForce);
			}
			else
			{
				overlappingSplotchGameObjects.RemoveAt(index);
			}
		}*/
	}
	
	public void Init(int a_Level, bool a_IsMachine = true)
	{
		ignoreMe = false;
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		
		splotchLevel = a_Level;
		ResetScale();
		
		if(a_IsMachine)
		{
			this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("oil");
			isMachine = true;
		}
		else
		{
			this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("grass");
		}
	}

	public void AddLevels(int a_NumLevels)
	{
		splotchLevel += a_NumLevels;
		if(splotchLevel <= 0)
		{
			gameController.IncrementEnemySplotchDestroyedCounter();
			gameController.DestroyGameObject(this.gameObject);
		}
		else
		{
			ResetScale();
		}
	}
	
	void ResetScale()
	{
		float newScale = splotchLevel;// < 50 ? splotchLevel : 50;
		newScale = newScale > 0 ? newScale : 0.1f;
		
		newScale -= 1;
		this.transform.localScale = new Vector3(1.0f + newScale / 10.0f, 1.0f + newScale / 10.0f, 1);
	}
}

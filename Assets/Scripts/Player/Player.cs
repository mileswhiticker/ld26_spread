using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	int[] contents;
	Camera mainCamera;
	public float maxSpeed = 5;
	public bool isMachine = true;
	public int playerLevel = 1;
	
	GameController gameController;
	
	void Start()
	{
		contents = new int[5];
		for(int i=0;i<5;++i)
		{
			contents[i] = 0;
		}
		//todo: init gui?
		
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	public void Upgrade()
	{
		playerLevel += 1;
		if(playerLevel > 0 && playerLevel <= 6)
		{
			if(isMachine)
			{
				this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("machine" + playerLevel);
			}
			else
			{
				this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("organic" + playerLevel);
			}
		}
	}
}

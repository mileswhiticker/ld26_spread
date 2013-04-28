using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	Camera mainCamera;
	public float maxSpeed = 5;
	public bool isMachine = true;
	public int playerLevel = 1;
	public float tLeftCantPickup = 0;
	
	public float tLeftToggleDrops = 0;
	
	public bool playerControlled = false;
	GameController gameController;
	public float energy = 10;
	
	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		energy = gameController.playerStartingEnergy;
	}
	
	public void ChangeLevel(int a_NewLevel = 0)
	{
		if(a_NewLevel > 0)
		{
			playerLevel = a_NewLevel;
		}
		else
		{
			playerLevel += 1;
		}
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

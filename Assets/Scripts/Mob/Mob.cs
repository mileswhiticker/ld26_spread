using UnityEngine;
using System.Collections;

public partial class Mob : MonoBehaviour
{
	public int mobLevel = 1;
	public bool isMachine = true;
	public bool destroyingEnemies = false;
	
	public float tLeftDropSplotch = 10.0f;
	
	public float tLeftAttracted = 0;
	public Player attractedPlayer;
	public float tLeftUnhealthy = 0;
	public float tLeftCantPickup = 0;
	
	public GameController gameController;
	
	//for the template
	public bool permaFrozen = true;
	
	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update()
	{
		if(tLeftUnhealthy > 0)
		{
			tLeftUnhealthy -= Time.deltaTime;
		}
		if(tLeftCantPickup > 0)
		{
			tLeftCantPickup -= Time.deltaTime;
		}
		
		if(!permaFrozen)
		{
			if(tLeftAttracted > 0)
			{
				tLeftAttracted -= Time.deltaTime;
				if(attractedPlayer && this.rigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed)
				{
					Vector3 targetDir = attractedPlayer.transform.position - this.transform.position;
					this.rigidbody.AddForce(targetDir.normalized);
				}
			}
			else
			{
				MoveToWanderTarget();
				tLeftDropSplotch -= Time.deltaTime;
				if(tLeftDropSplotch <= 0 && gameController.enemySplotchProgress < gameController.splotchesToWin)
				{
					tLeftDropSplotch = 10 - mobLevel;
					Vector3 spawnPos = this.transform.position;
					spawnPos.z = 0;
					gameController.CreateSplotch(spawnPos, mobLevel, isMachine);
				}
			}
		}
	}
	
	public void DownGrade()
	{
		//when hit by the player, if they're more powerful than us
		tLeftCantPickup = 5.0f;
		mobLevel -= 1;
		gameController.CreateSparkly(this.transform.position);
		//newSparkly.rigidbody.AddForce(-25 + Random.value * 50, -25 + Random.value * 50, 0);
		/*for(int i=0;i<mobLevel + 1;++i)
		{
			Sparkly newSparkly = gameController.CreateSparkly(this.transform.position);
			newSparkly.rigidbody.AddForce(-25 + Random.value * 50, -25 + Random.value * 50, 0);
		}*/
		if(mobLevel > 0)
		{
			SetLevel(mobLevel);
		}
		else
		{
			gameController.DestroyGameObject(this.gameObject);
		}
	}
	
	public void Init(bool a_IsMachine, bool a_IsFriend)
	{
		isMachine = a_IsMachine;
		permaFrozen = false;
		
		SetLevel(mobLevel);
		if(a_IsFriend)
		{
			renderer.material.SetColor("_Color",Color.blue);
		}
		else
		{
			renderer.material.SetColor("_Color",Color.red);
		}
		wanderTarget = gameController.GetRandomMapPoint();
	}
	
	public void SetLevel(int a_NewLevel)
	{
		mobLevel = a_NewLevel;
		if(mobLevel > 0 && mobLevel <= 6)
		{
			if(isMachine)
			{
				this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("machine" + mobLevel);
			}
			else
			{
				this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("organic" + mobLevel);
			}
		}
		else
		{
			GameController.DestroyObject(this.gameObject);
		}
	}
}

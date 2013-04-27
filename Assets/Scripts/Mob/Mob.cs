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
	
	//for the template
	public bool permaFrozen = true;
	
	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		wanderTarget = gameController.GetRandomMapPoint();
	}
	
	void Update()
	{
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
				if(tLeftDropSplotch <= 0)
				{
					tLeftDropSplotch = 10 - mobLevel;
					Vector3 spawnPos = this.transform.position;
					spawnPos.z = 0;
					gameController.CreateSplotch(spawnPos, isMachine);
				}
			}
		}
	}
	
	public void Init(bool a_IsMachine, bool a_IsFriend)
	{
		isMachine = a_IsMachine;
		permaFrozen = false;
		
		if(mobLevel > 0 && mobLevel <= 6)
		{
			if(a_IsMachine)
			{
				this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("machine" + mobLevel);
			}
			else
			{
				this.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("organic" + mobLevel);
			}
		}
		
		if(!a_IsFriend)
		{
			renderer.material.SetColor("_Color",Color.red);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameController : MonoBehaviour
{
	GameObject splotchTemplate;
	List<Splotch> friendlySplotches;
	List<Splotch> enemySplotches;
	
	public float tLeftNextFriendlySpawn = 0.5f;
	public float tLeftNextEnemySpawn = 0.5f;
	
	GameObject sparklyTemplate;
	List<Sparkly> sparklies;
	public float targetSparklyPercentage = 0.5f;	//% of splotches required to win
	public float tLeftNextSparklySpawn = 1.5f;
	
	GameObject mobTemplate;
	List<Mob> friendlyMobs;
	List<Mob> enemyMobs;
	
	Player player;
	
	public float winPercent = 0;
	public bool updateWinPercentNext = false;
	
	public Vector3[] wanderTargets;
	
	//todo: calc this according to level size (difficulty?)
	//half of the total area in m^2 seems to be a good amount (basically everything covered)
	//area is 40mx40m, or 1600m^2
	int splotchesToWin = 800;
	
	void Start()
	{
		splotchTemplate = GameObject.FindGameObjectWithTag("splotchTemplate");
		friendlySplotches = new List<Splotch>();
		enemySplotches = new List<Splotch>();
		
		mobTemplate = GameObject.FindGameObjectWithTag("mobTemplate");
		friendlyMobs = new List<Mob>();
		enemyMobs = new List<Mob>();
		
		sparklyTemplate = GameObject.FindGameObjectWithTag("sparklyTemplate");
		sparklies = new List<Sparkly>();
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		wanderTargets = new Vector3[8];
		wanderTargets[0] = new Vector3(-15,-15, mobTemplate.transform.position.z);
		wanderTargets[1] = new Vector3(-15,15, mobTemplate.transform.position.z);
		wanderTargets[2] = new Vector3(-15,0, mobTemplate.transform.position.z);
		wanderTargets[3] = new Vector3(15,-15, mobTemplate.transform.position.z);
		wanderTargets[4] = new Vector3(15,15, mobTemplate.transform.position.z);
		wanderTargets[5] = new Vector3(15,0, mobTemplate.transform.position.z);
		wanderTargets[6] = new Vector3(0,15, mobTemplate.transform.position.z);
		wanderTargets[7] = new Vector3(0,-15, mobTemplate.transform.position.z);
	}
	
	void Update()
	{
		//populate friendly mobs
		if(tLeftNextFriendlySpawn <= 0)
		{
			if(friendlyMobs.Count < splotchesToWin / 100)
			{
				//Debug.Log("adding friendly mob");
				friendlyMobs.Add(CreateMob(player.isMachine));
				tLeftNextFriendlySpawn = 0.5f;
			}
		}
		else
		{
			tLeftNextFriendlySpawn -= Time.deltaTime;
		}
		
		//populate enemy mobs
		if(tLeftNextEnemySpawn <= 0)
		{
			if(enemyMobs.Count < player.playerLevel * 5)
			{
				enemyMobs.Add(CreateMob(!player.isMachine));
				tLeftNextEnemySpawn = 0.5f;
			}
		}
		else
		{
			tLeftNextEnemySpawn -= Time.deltaTime;
		}
		
		//spawn a sparkly
		if(tLeftNextSparklySpawn <= 0)
		{
			SpawnNewSparkly();
			tLeftNextSparklySpawn = 2.5f + Random.value * 2.5f;
		}
		else
		{
			if((float)sparklies.Count / (float)splotchesToWin <  targetSparklyPercentage)
			tLeftNextSparklySpawn -= Time.deltaTime;
		}
		
		//update win percentage
		if(updateWinPercentNext)
		{
			winPercent = (float)(friendlySplotches.Count - enemySplotches.Count) / (float)splotchesToWin;
			updateWinPercentNext = false;
			//Debug.Log("Progress: " + (float)splotches.Count / (float)splotchesToWin);
			
			//winning and upgrading
			if(winPercent >= 1)
			{
				//todo: win
			}
			else if(winPercent * 100 > player.playerLevel * 15)
			{
				player.Upgrade();
			}
		}
	}
}

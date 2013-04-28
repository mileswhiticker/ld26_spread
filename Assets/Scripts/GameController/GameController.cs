using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameController : MonoBehaviour
{
	//splotches
	GameObject splotchTemplate;
	List<Splotch> friendlySplotches;
	List<Splotch> enemySplotches;
	
	//sparklies
	GameObject sparklyTemplate;
	List<Sparkly> sparklies;
	float targetSparklyPercentage = 0.5f;	//% of splotches required to win
	float tLeftNextSparklySpawn = 1.5f;
	
	//mobs
	GameObject mobTemplate;
	List<Mob> friendlyMobs;
	List<Mob> enemyMobs;
	Vector3[] wanderTargets;
	float tLeftNextFriendlySpawn = 0.5f;
	float tLeftNextEnemySpawn = 0.5f;
	
	Player player;
	public int playerStartingEnergy = 25;
	
	//levels, completion and progress
	/*todo: calc splotchesToWin according to level size (difficulty?)
	3/4 of the total area in m^2 seems to be a good amount (basically everything covered)
	default area is 40mx40m, or 1600m^2*/
	public int splotchesToWin = 1600;
	int curLevel = 0;
	public int friendlySplotchProgress = 0;
	public int enemySplotchProgress = 0;
	
	//GUI
	public GameObject organicProgressBar;
	public GameObject machineProgressBar;
	public GameObject energyBar;
	public GameObject machine;
	public GameObject tree;
	public GameObject teamMarker;
	public GameObject scoreBackground;
	public GameObject cancel;
	public float scoreMoveProgress = 1;
	public int scoreMoveDirection = 0;
	Texture2D textureMachine;
	Texture2D textureTree;
	Texture2D greenTex;
	Texture2D redTex;
	bool handleMenu = true;
	float lastOnGUITime = 0;
	
	//scoring
	int[] friendlyBumpScores;	//unused
	int[] enemyBumpScores;		//unused
	int sparkliesEaten = 0;
	float timeTaken = 0;
	int enemySplotchCreated = 0;
	int enemySplotchDestroyed = 0;
	int friendlySplotchCreated = 0;
	int friendlySplotchDestroyed = 0;
	bool won = true;
	bool playerIsMachineNextGame = true;
	
	void Start()
	{
		textureMachine = (Texture2D)Resources.Load("machine");
		textureMachine.filterMode = FilterMode.Point;
		textureTree = (Texture2D)Resources.Load("tree");
		textureTree.filterMode = FilterMode.Point;
		
		greenTex = (Texture2D)Resources.Load("col_green");
		redTex = (Texture2D)Resources.Load("col_red");
		
		teamMarker.renderer.material.mainTexture.filterMode = FilterMode.Point;
		
		friendlyBumpScores = new int[6];
		enemyBumpScores = new int[6];
		
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
		if(curLevel > 0)
		{
			timeTaken += Time.deltaTime;
			
			//populate friendly mobs
			//tLeftNextFriendlySpawn = 9.0f;
			if(tLeftNextFriendlySpawn <= 0)
			{
				if(friendlyMobs.Count < splotchesToWin / 100)
				{
					friendlyMobs.Add(CreateMob(player.isMachine));
					tLeftNextFriendlySpawn = 0.5f;
				}
			}
			else
			{
				tLeftNextFriendlySpawn -= Time.deltaTime;
			}
			
			//populate enemy mobs
			//tLeftNextEnemySpawn = 9.0f;
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
				tLeftNextSparklySpawn = 1.5f + Random.value * 2.5f;
			}
			else
			{
				if((float)sparklies.Count / (float)splotchesToWin <  targetSparklyPercentage)
				tLeftNextSparklySpawn -= Time.deltaTime;
			}
			
			//update win percentage
			/*if(updateWinPercentNext)
			{
				winPercent = (float)(friendlySplotches.Count - enemySplotches.Count) / (float)splotchesToWin;
				updateWinPercentNext = false;
				//Debug.Log("Progress: " + (float)splotches.Count / (float)splotchesToWin);
				
				//winning and upgrading
				if(winPercent >= 1)
				{
					curLevel = 0;
					player.playerControlled = false;
					ResetLevel();
				}
				else if(winPercent * 100 > player.playerLevel * 15)
				{
					player.ChangeLevel();
				}
			}*/
		}
	}
	
	public void IncrementSparklyCounter()
	{
		if(curLevel > 0)
			sparkliesEaten += 1;
	}
	public void IncrementEnemySplotchCreatedCounter()
	{
		if(curLevel > 0)
			enemySplotchCreated += 1;
	}
	public void IncrementEnemySplotchDestroyedCounter()
	{
		if(curLevel > 0)
			enemySplotchDestroyed += 1;
	}
	public void IncrementFriendlyBump(int a_Level)
	{
		if(curLevel > 0)
			friendlyBumpScores[a_Level] += 1;
	}
	public void IncrementEnemyBump(int a_Level)
	{
		if(curLevel > 0)
			enemyBumpScores[a_Level] += 1;
	}
}

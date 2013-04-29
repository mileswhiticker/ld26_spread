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
	float tLeftUpdateHelpers = 0;
	
	//GUI
	public GameObject organicProgressBar;
	public GameObject machineProgressBar;
	public GameObject energyBar;
	public GameObject machine;
	public GameObject tree;
	public GameObject teamMarker;
	public GameObject scoreBackground;
	public GameObject cancel;
	public GameObject door;
	public GameObject progressBorder;
	public float scoreMoveProgress = 1;
	public int scoreMoveDirection = 0;
	Texture2D textureMachine;
	Texture2D textureTree;
	Texture2D greenTex;
	Texture2D redTex;
	bool handleMenu = true;
	float lastOnGUITime = 0;
	
	public GameObject topBorder;
	public GameObject bottomBorder;
	public GameObject leftBorder;
	public GameObject rightBorder;
	
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
	string[] quotes;
	
	Sparkly sparklyTarget;
	GameObject gameObjectTarget;
	public GameObject sparklyLine;
	public GameObject mobLine;
	
	//sound
	public AudioClip chargeup;
	public AudioClip pickup;
	public AudioClip blip;
	public AudioClip dropSplotch;
	public AudioClip upgrade;
	public AudioClip fail;
	public AudioClip win;
	
	void Start()
	{
		quotes = new string[11];
		quotes[0] = "Be Content with what you have; rejoice in the way things are. When you realize there is nothing lacking, the whole world belongs to you. - Lao Tzu";
		quotes[1] = "It looks like you can write a minimalist piece without much bleeding. And you can. But not a good one. - David Foster Wallace";
		quotes[2] = "The secret of happiness, you see, is not found in seeking more, but in developing the capacity to enjoy less. - Socrates";
		quotes[3] = "Nature does not hurry, yet everything is accomplished. - Lao Tzu";
		quotes[4] = "A good traveller has no fixed plans, and is not intent on arriving. - Lao Tzu";
		quotes[5] = "Less is more. - Ludwig Mies van der Rohe";
		quotes[6] = "The ability to simplify means to eliminate the unnecessary so that the necessary may speak. - Hans Hofmann";
		quotes[7] = "Life is really simple, but we insist on making it complicated. - Confucius";
		quotes[8] = "People love chopping wood. In this activity one immediately sees results. - Albert Einstein";
		quotes[9] = "There's more to some things than meets the eye. - Anonymous";
		quotes[10] = "Potatoes. - Ludum Dare 26 Competitor";
		//quotes[5] = "asdasd";
		
		textureMachine = (Texture2D)Resources.Load("machine");
		textureMachine.filterMode = FilterMode.Point;
		textureTree = (Texture2D)Resources.Load("tree");
		textureTree.filterMode = FilterMode.Point;
		
		sparklyAttract = sparklyLine.GetComponent<Attract>();
		mobAttract = sparklyLine.GetComponent<Attract>();
		
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
}

using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	Attract sparklyAttract;
	Attract mobAttract;
	
	float tLeftChangeQuote = 0;
	int curQuoteIndex = -1;
	public string curQuote = "";
	float tLeftCurHint = 0;
	float tLeftShowHint = 0;
	
	void Update()
	{
		if(curLevel > 0)
		{
			timeTaken += Time.deltaTime;
			
			//force show hints if the player's energy is low
			if(player.energy < player.energyPerSplotch)	
			{	
				Vector3 newScale = sparklyLine.transform.localScale;
				newScale.y = 1.0f;
				sparklyLine.transform.localScale = newScale;
				
				newScale = mobLine.transform.localScale;
				newScale.y = 1.0f;
				mobLine.transform.localScale = newScale;
			}
			else
			{
				if(tLeftShowHint <= 0)
				{
					tLeftCurHint -= Time.deltaTime;
					if(tLeftCurHint <= 0)
					{
						//hide hints for next 15 seconds
						Vector3 newScale = sparklyLine.transform.localScale;
						newScale.y = 0.0f;
						sparklyLine.transform.localScale = newScale;
						
						newScale = mobLine.transform.localScale;
						newScale.y = 0.0f;
						mobLine.transform.localScale = newScale;
						
						tLeftShowHint = 6.0f;
					}
				}
				else
				{
					tLeftShowHint -= Time.deltaTime;
					if(tLeftShowHint <= 0)
					{
						//show hints for next 5 seconds
						Vector3 newScale = sparklyLine.transform.localScale;
						newScale.y = 1.0f;
						sparklyLine.transform.localScale = newScale;
						
						newScale = mobLine.transform.localScale;
						newScale.y = 1.0f;
						mobLine.transform.localScale = newScale;
						
						tLeftCurHint = 3.0f;
					}
				}
			}
			
			//update helper lines
			if(sparklyTarget)
			{
				Vector3 moveTarget = sparklyTarget.transform.position;
				if((sparklyTarget.transform.position - player.transform.position).sqrMagnitude > 25)
				{
					moveTarget = player.transform.position + (sparklyTarget.transform.position - player.transform.position).normalized * 5.0f;
				}
				sparklyLine.transform.position += (moveTarget - sparklyLine.transform.position).normalized * Time.deltaTime * 5;
				sparklyLine.renderer.material.mainTexture = sparklyAttract.attract;
			}
			else
			{
				//sparklyLine.renderer.material.mainTexture = sparklyAttract.thinking;
			}
			
			if(gameObjectTarget)
			{
				Vector3 moveTarget = gameObjectTarget.transform.position;
				if((gameObjectTarget.transform.position - player.transform.position).sqrMagnitude > 25)
				{
					moveTarget = player.transform.position + (gameObjectTarget.transform.position - player.transform.position).normalized * 5.0f;
				}
				mobLine.transform.position += (moveTarget - mobLine.transform.position).normalized * Time.deltaTime * 5;
				mobLine.renderer.material.mainTexture = mobAttract.attract;
			}
			else
			{
				//mobLine.renderer.material.mainTexture = mobAttract.thinking;
			}
			
			if(tLeftUpdateHelpers <= 0)
			{
				//recheck sparklies to find the closest
				foreach(Sparkly sparkly in sparklies)
				{
					if(!sparklyTarget || 
						(sparklyTarget.transform.position - player.transform.position).sqrMagnitude > 
						(sparkly.transform.position - player.transform.position).sqrMagnitude )
					{
						sparklyTarget = sparkly;
					}
				}
				//recheck mobs to find the closest viable
				foreach(Mob mob in friendlyMobs)
				{
					GameObject curGameObject = mob.gameObject;
					if(mob.mobLevel < player.playerLevel && (!gameObjectTarget || 
						(gameObjectTarget.transform.position - player.transform.position).sqrMagnitude > 
						(curGameObject.transform.position - player.transform.position).sqrMagnitude) )
					{
						gameObjectTarget = curGameObject;
					}
				}
				//
				tLeftUpdateHelpers = 0.1f;
			}
			else
			{
				tLeftUpdateHelpers -= Time.deltaTime;
			}
			
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
}

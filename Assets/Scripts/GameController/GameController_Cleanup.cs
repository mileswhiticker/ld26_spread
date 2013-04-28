using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void DestroyGameObject(GameObject a_GameObjectToDestroy)
	{
		Mob mob = a_GameObjectToDestroy.GetComponent<Mob>();
		if(mob)
		{
			if(mob.isMachine == player.isMachine)
			{
				friendlyMobs.Remove(mob);
			}
			else
			{
				enemyMobs.Remove(mob);
			}
			goto end;
		}
		
		Sparkly sparkly = a_GameObjectToDestroy.GetComponent<Sparkly>();
		if(sparkly)
		{
			sparklies.Remove(sparkly);
			goto end;
		}
		
		Splotch splotch = a_GameObjectToDestroy.GetComponent<Splotch>();
		if(splotch)
		{
			if(splotch.isMachine == player.isMachine)
			{
				friendlySplotches.Remove(splotch);
				friendlySplotchDestroyed += 1;
			}
			else
			{
				enemySplotches.Remove(splotch);
				enemySplotchDestroyed += 1;
			}
			goto end;
		}
		
		end:
		{
			Destroy(a_GameObjectToDestroy);
		};
	}
	
	void ResetLevel()
	{
		foreach(Mob mob in friendlyMobs)
		{
			Destroy(mob.gameObject);
		}
		friendlyMobs.Clear();
		foreach(Mob mob in enemyMobs)
		{
			Destroy(mob.gameObject);
		}
		enemyMobs.Clear();
		foreach(Sparkly sparkly in sparklies)
		{
			Destroy(sparkly.gameObject);
		}
		sparklies.Clear();
		foreach(Splotch splotch in friendlySplotches)
		{
			Destroy(splotch.gameObject);
		}
		friendlySplotches.Clear();
		foreach(Splotch splotch in enemySplotches)
		{
			Destroy(splotch.gameObject);
		}
		enemySplotches.Clear();
		player.ChangeLevel(1);
		player.releasingSplotches = false;
		player.energy = playerStartingEnergy;
		
		//reset score
		for(int i=0;i<6;++i)
			friendlyBumpScores[i] = 0;
		for(int i=0;i<6;++i)
			enemyBumpScores[i] = 0;
		sparkliesEaten = 0;
		timeTaken = 0;
		enemySplotchCreated = 0;
		enemySplotchDestroyed = 0;
		friendlySplotchProgress = 0;
		enemySplotchProgress = 0;
		
		//reset GUI
		//
		Vector3 newScale = machineProgressBar.transform.localScale;
		newScale.x = 0;
		machineProgressBar.transform.localScale = newScale;
		//
		Vector3 newPos = organicProgressBar.transform.localPosition;
		newPos.x = 0;
		organicProgressBar.transform.localPosition = newPos;
		//
		newScale = organicProgressBar.transform.localScale;
		newScale.x = 0;
		organicProgressBar.transform.localScale = newScale;
		//
		newPos = machineProgressBar.transform.localPosition;
		newPos.x = 0;
		machineProgressBar.transform.localPosition = newPos;
		if(scoreMoveProgress > 0)
			scoreMoveDirection = -1;
		//
		//tree.renderer.material.SetColor("_Color",Color.white);
		//machine.renderer.material.SetColor("_Color",Color.white);
		//
		if(player.isMachine)
		{
			machineProgressBar.renderer.material.mainTexture = greenTex;
			organicProgressBar.renderer.material.mainTexture = redTex;
		}
		else
		{
			machineProgressBar.renderer.material.mainTexture = redTex;
			organicProgressBar.renderer.material.mainTexture = greenTex;
		}
	}
}

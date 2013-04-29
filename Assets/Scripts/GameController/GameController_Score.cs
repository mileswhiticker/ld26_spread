using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void IncrementSparklyCounter()
	{
		if(curLevel > 0)
			sparkliesEaten += 1;
		
		//pseudohaptic feedback!
		Vector3 newScale = energyBar.transform.localScale;
		newScale.y *= 2;
		energyBar.transform.localScale = newScale;
		
		audio.PlayOneShot(chargeup);
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

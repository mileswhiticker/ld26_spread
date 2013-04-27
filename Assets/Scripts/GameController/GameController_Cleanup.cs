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
			}
			else
			{
				enemySplotches.Remove(splotch);
			}
			goto end;
		}
		
		end:
		{
			Destroy(a_GameObjectToDestroy);
		};
	}
}

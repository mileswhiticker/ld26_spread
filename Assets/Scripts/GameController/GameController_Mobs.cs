using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	Mob CreateMob(bool a_IsMachine)
	{
		//todo: spawn these where the player can't see them
		GameObject newMobGO = (GameObject)Instantiate(mobTemplate, GetRandomMapPoint(), Quaternion.identity);
		Mob newMob = newMobGO.GetComponent<Mob>();
		
		if(Random.value > 0.33)
		{
			newMob.mobLevel = player.playerLevel > 1 ? player.playerLevel - 1 : player.playerLevel;
		}
		else if(Random.value > 0.5)
		{
			newMob.mobLevel = player.playerLevel;
		}
		else
		{
			newMob.mobLevel = player.playerLevel < 6 ? player.playerLevel + 1 : player.playerLevel;
		}
		newMob.Init(a_IsMachine, a_IsMachine == player.isMachine);
		
		return newMob;
	}
}

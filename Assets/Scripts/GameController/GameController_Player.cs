using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void PlayerAttract()
	{
		foreach(Mob curMob in friendlyMobs)
		{
			if(curMob)
			{
				curMob.attractedPlayer = player;
				curMob.tLeftAttracted = 5.0f;
			}
			else
			{
				//something got deleted improperly, better clean it out
				friendlyMobs.Remove(curMob);
			}
		}
	}
}

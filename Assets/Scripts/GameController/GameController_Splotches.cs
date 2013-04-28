using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void CreateSplotch(Vector3 a_SpawnPos, int a_Level = 1, bool a_IsMachine = true)
	{
		GameObject newSplotchGO = (GameObject)Instantiate(splotchTemplate, a_SpawnPos, this.transform.rotation);
		if(newSplotchGO)
		{
			Splotch newSplotch = newSplotchGO.GetComponent<Splotch>();
			newSplotch.Init(a_Level, a_IsMachine);
			if(player.isMachine == a_IsMachine)
			{
				friendlySplotches.Add(newSplotch);
				friendlySplotchProgress += newSplotch.splotchLevel;
				friendlySplotchCreated += newSplotch.splotchLevel;
				UpdateFriendlySplotchProgress();
			}
			else
			{
				enemySplotches.Add(newSplotch);
				enemySplotchProgress += newSplotch.splotchLevel;
				enemySplotchCreated += newSplotch.splotchLevel;
				UpdateEnemySplotchProgress();
			}
			//updateWinPercentNext = true;
		}
	}
}

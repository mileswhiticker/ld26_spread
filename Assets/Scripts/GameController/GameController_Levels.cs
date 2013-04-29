using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	void BeginLevel(int a_Level)
	{
		if(a_Level < 1)
		{
			a_Level = 1;
		}
		
		if(playerIsMachineNextGame)
		{
			player.isMachine = true;
		}
		else
		{
			player.isMachine = false;
		}
		
		ResetLevel();
		ShowGui();
		
		curLevel = 1;
		player.playerControlled = true;
		handleMenu = false;
		
		//place a sparkly somewhere near the player
		Vector3 spawnPos = sparklyTemplate.transform.position;
		spawnPos.x = player.transform.position.x + 250 + Random.value * (Screen.width - 250);
		spawnPos.y = player.transform.position.y + 250 + Random.value * (Screen.height - 250);
		CreateSparkly(spawnPos);
	}
}

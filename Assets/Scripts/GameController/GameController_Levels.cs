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
	}
}

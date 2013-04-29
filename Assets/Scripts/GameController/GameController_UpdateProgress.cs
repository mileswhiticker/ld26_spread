using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	
	void UpdateFriendlySplotchProgress()
	{
		if(curLevel != 0)
		{
			//winning and upgrading
			float percentWon = (float)friendlySplotchProgress / (float)splotchesToWin;
			if(percentWon >= 1)
			{
				//player won
				GameOver(true);
			}
			else if(percentWon * 100 > player.playerLevel * 17)
			{
				audio.PlayOneShot(upgrade);
				player.ChangeLevel();
			}
			
			//update the bar transform
			//scale of 6 = half the screen's width
			if(player.isMachine)
			{
				UpdateMachineBar(percentWon);
			}
			else
			{
				UpdateOrganicBar(percentWon);
			}
		}
	}
	
	
	void UpdateEnemySplotchProgress()
	{
		if(curLevel != 0)
		{
			//winning and upgrading
			float percentWon = (float)enemySplotchProgress / (float)splotchesToWin;
			if(percentWon >= 1)
			{
				//player lost
				GameOver(false);
			}
			//no enemy upgrades... yet
			/*else if(percentWon * 100 > player.playerLevel * 17)
			{
				player.ChangeLevel();
			}*/
			
			//update the bar transform
			//scale of 6 = half the screen's width
			if(player.isMachine)
			{
				UpdateOrganicBar(percentWon);
			}
			else
			{
				UpdateMachineBar(percentWon);
			}
		}
	}
}

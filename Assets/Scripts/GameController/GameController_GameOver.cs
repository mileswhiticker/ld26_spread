using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	void GameOver(bool a_Won)
	{
		won = a_Won;
		curLevel = 0;
		player.playerControlled = false;
		ShowMenu();
		scoreMoveDirection = 1;
		
		Vector3 newScale = cancel.transform.localScale;
		newScale.y = 0;
		cancel.transform.localScale = newScale;
		
		string bgTexName = "red_half_trans";
		if(won)
		{
			bgTexName = "green_half_trans";
		}
		//teamTexture = (Texture2D)Resources.Load(teamTexName + Mathf.Ceil(Random.value * 3.0f));
		scoreBackground.renderer.material.mainTexture = (Texture2D)Resources.Load(bgTexName);
	}
	
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

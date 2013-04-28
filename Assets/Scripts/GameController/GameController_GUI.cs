using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	void OnGUI()
	{
		float deltaT = Time.realtimeSinceStartup - lastOnGUITime;
		lastOnGUITime = Time.realtimeSinceStartup;
		//scoreboard is moving
		if(scoreMoveDirection != 0)
		{
			//MoveScoreBackground(scoreMoveProgress);
			scoreMoveProgress += scoreMoveDirection * deltaT / 2;
			Vector3 newPos = scoreBackground.transform.localPosition;
			newPos.y = 12 - 12 * scoreMoveProgress;
			if(scoreMoveProgress >= 1 || scoreMoveProgress <= 0)
			{
				if(scoreMoveProgress > 1)
				{
					scoreMoveProgress = 1;
					handleMenu = true;
				}
				else if(scoreMoveProgress < 1)
					scoreMoveProgress = 0;
				newPos.y = 12 - 12 * scoreMoveProgress * scoreMoveProgress;
				scoreMoveDirection = 0;
			}
			scoreBackground.transform.localPosition = newPos;
			
			Vector3 pos1 = new Vector3(-5.5f, -5.5f, 1.0f);
			Vector3 pos2 = new Vector3(-3.5f, 0.0f, 1.0f);
			newPos = pos1 + scoreMoveProgress * (pos2 - pos1);
			tree.transform.localPosition = newPos;
			//
			pos1 = new Vector3(5.5f, -5.5f, 1.0f);
			pos2 = new Vector3(3.5f, 0.0f, 1.0f);
			newPos = pos1 + scoreMoveProgress * (pos2 - pos1);
			machine.transform.localPosition = newPos;
			
			//Debug.Log(scoreMoveProgress + " : " + (12 - 12 * scoreMoveProgress));
		}
		else if(scoreMoveProgress == 0)
		{
			//ingame pause button (click anywhere on the screen)
			if(GUI.Button(new Rect(
				0,
				0,
				Screen.width,
				Screen.height),
				"",
				GUIStyle.none))
			/*if(GUI.Button(new Rect(
				5 * Screen.width / 12.0f,
				5 * Screen.height / 12.0f ,
				Screen.width / 6.0f,
				Screen.height / 6.0f),
				"",
				GUIStyle.none))*/
			{
				//teamTexture = (Texture2D)Resources.Load(teamTexName + Mathf.Ceil(Random.value * 3.0f));
				scoreBackground.renderer.material.mainTexture = (Texture2D)Resources.Load("white_half_trans");
				
				Vector3 newScale = cancel.transform.localScale;
				newScale.y = 1;
				cancel.transform.localScale = newScale;
				
				ShowMenu();
				Time.timeScale = 0;
			}
		}
		
		if(handleMenu)
		{
			HandleMenus(deltaT);
		}
		else if(player.playerControlled)
		{
			//update energy display bar
			Vector3 newScale = energyBar.transform.localScale;
			if(player.energy < player.energyPerSplotch)
			{
				newScale.x = 0;
			}
			else
			{
				newScale.x = player.energy / 5.0f;
			}
			energyBar.transform.localScale = newScale;
			
			/*
			//placeholder energy gui
			float boxWidth = Screen.width / 3;
			float energyLeft = player.energy;
			if(boxWidth < 150)
				energyLeft = Mathf.Round(energyLeft);
			GUI.Box(new Rect(10,10,Screen.width / 3,25), "Energy: " + energyLeft);
			
			//placeholder progress bar
			float percentWon = (float)splotchProgress / (float)splotchesToWin;
			if(boxWidth < 150)
				percentWon = Mathf.Round(percentWon);
			GUI.Box(new Rect(Screen.width / 2.0f + 10,10,Screen.width / 3,25), "Progress: " + percentWon * 100 + "%");
			*/
		}
	}
	
	void ShowMenu()
	{
		scoreMoveDirection = 1;
		Vector3 newScale = machineProgressBar.transform.localScale;
		newScale.y = 0;
		machineProgressBar.transform.localScale = newScale;
		
		newScale = organicProgressBar.transform.localScale;
		newScale.y = 0;
		organicProgressBar.transform.localScale = newScale;
		
		newScale = energyBar.transform.localScale;
		newScale.y = 0;
		energyBar.transform.localScale = newScale;
		
		/*newScale = machine.transform.localScale;
		newScale.y = 0;
		machine.transform.localScale = newScale;
		
		newScale = tree.transform.localScale;
		newScale.y = 0;
		tree.transform.localScale = newScale;*/
	}
	
	void ShowGui()
	{
		scoreMoveDirection = -1;
		Vector3 newScale = organicProgressBar.transform.localScale;
		newScale.y = 0.75f;
		organicProgressBar.transform.localScale = newScale;
		
		newScale = machineProgressBar.transform.localScale;
		newScale.y = 0.75f;
		machineProgressBar.transform.localScale = newScale;
		
		newScale = energyBar.transform.localScale;
		newScale.y = 0.5f;
		energyBar.transform.localScale = newScale;
		
		/*newScale = machine.transform.localScale;
		newScale.y = 1;
		machine.transform.localScale = newScale;
		
		newScale = tree.transform.localScale;
		newScale.y = 1;
		tree.transform.localScale = newScale;*/
	}
	
	void UpdateMachineBar(float a_PercentProgress)
	{
		Vector3 newScale = machineProgressBar.transform.localScale;
		newScale.x = a_PercentProgress * 5.0f;
		machineProgressBar.transform.localScale = newScale;
		//
		Vector3 newPos = machineProgressBar.transform.localPosition;
		newPos.x = a_PercentProgress * 2.5f;
		machineProgressBar.transform.localPosition = newPos;
	}
	
	void UpdateOrganicBar(float a_PercentProgress)
	{
		Vector3 newScale = organicProgressBar.transform.localScale;
		newScale.x = -a_PercentProgress * 5.0f;
		organicProgressBar.transform.localScale = newScale;
		//
		Vector3 newPos = organicProgressBar.transform.localPosition;
		newPos.x = -a_PercentProgress * 2.5f;
		organicProgressBar.transform.localPosition = newPos;
	}
}

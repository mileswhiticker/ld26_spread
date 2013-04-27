using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	void OnGUI()
	{
		if(player)
		{
			float boxWidth = Screen.width / 3;
			float val = player.energy;
			if(boxWidth < 150)
				val = Mathf.Round(val);
			GUI.Box(new Rect(10,10,Screen.width / 3,25), "Energy: " + val);
			
			val = winPercent;
			if(boxWidth < 150)
				val = Mathf.Round(val);
			GUI.Box(new Rect(Screen.width / 2.0f + 10,10,Screen.width / 3,25), "Progress: " + val * 100 + "%");
		}
		/*
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			Application.LoadLevel(2);
		}
		*/
	}
}
